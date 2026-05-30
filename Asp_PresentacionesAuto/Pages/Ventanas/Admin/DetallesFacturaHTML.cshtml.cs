using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class DetallesFacturaHTMLModel : PageModel
    {
        private IDetallesFacturaPresentacion? IDetallesFacturaPresentacion;
        private IFacturasPresentacion? IFacturasPresentacion;
        [BindProperty] public List<DetallesFactura>? Lista { get; set; }
        [BindProperty] public List<Facturas>? ListaFactura { get; set; }
        [BindProperty] public DetallesFactura? DetallesFactura { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public DetallesFacturaHTMLModel()
        {
            IDetallesFacturaPresentacion = new DetallesFacturaPresentacion();
            IFacturasPresentacion = new FacturasPresentacion();
        }
        private void IniciarDetallesFactura()
        {
            var correo = HttpContext.Session.GetString("Usuario") ?? "Sistema";
            IDetallesFacturaPresentacion = new DetallesFacturaPresentacion(correo);
        }


        public List<Facturas> ObtenerFacturas()
        {
            return ListaFactura = IFacturasPresentacion!.Consultar();
        }

        private void CargarListaFiltrada()
        {
            var rol = HttpContext.Session.GetString("RolId");
            var entidadId = HttpContext.Session.GetString("EntidadId");

            Lista = IDetallesFacturaPresentacion!.Consultar();

            if (rol == "2" && int.TryParse(entidadId, out int clienteId))
            {
                var facturasCliente = IFacturasPresentacion!.Consultar()
                    .Where(f => f.Clientes == clienteId)
                    .Select(f => f.Id)
                    .ToList();

                Lista = Lista!.Where(x => facturasCliente.Contains(x.Facturas)).ToList();
            }
        }


        public void OnGet()
        {
            IniciarDetallesFactura();
            try
            {
                CargarListaFiltrada();
                DetallesFactura = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtRefrescar()
        {
            IniciarDetallesFactura();
            try
            {
                CargarListaFiltrada();
                DetallesFactura = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            IniciarDetallesFactura();
            DetallesFactura = new DetallesFactura();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            IniciarDetallesFactura();
            try
            {
                var rol = HttpContext.Session.GetString("RolId");
                var entidadId = HttpContext.Session.GetString("EntidadId");

                var listaTemp = IDetallesFacturaPresentacion!.Consultar();

                if (rol == "1")
                {
                    DetallesFactura = listaTemp!.FirstOrDefault(x => x.Id == data);
                }
                else if (rol == "2" && int.TryParse(entidadId, out int clienteId))
                {
                    var facturasCliente = IFacturasPresentacion!.Consultar()
                        .Where(f => f.Clientes == clienteId)
                        .Select(f => f.Id)
                        .ToList();

                    DetallesFactura = listaTemp!.FirstOrDefault(x =>
                        x.Id == data && facturasCliente.Contains(x.Facturas));
                }

                if (DetallesFactura == null)
                    ViewData["Mensaje"] = "No tienes permiso para modificar este detalle.";

                Lista = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }


        public void OnPostBtGuardar()
        {
            IniciarDetallesFactura();
            try
            {
                if (DetallesFactura == null)
                    return;

                if (DetallesFactura.Id == 0)
                    DetallesFactura = IDetallesFacturaPresentacion!.Guardar(DetallesFactura!);
                else
                    DetallesFactura = IDetallesFacturaPresentacion!.Modificar(DetallesFactura!);

                if (DetallesFactura.Id == 0)
                {
                    ViewData["Mensaje"] = "No fue posible guardar el detalle de factura.";
                    return;
                }

                ViewData["Mensaje"] = "Detalle de factura guardado correctamente.";

                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                Exception errorReal = ex;

                while (errorReal.InnerException != null)
                    errorReal = errorReal.InnerException;

                ViewData["Mensaje"] = errorReal.Message;

                OnPostBtRefrescar();
            }
        }

        public void OnPostBtBorrar()
        {
            IniciarDetallesFactura();
            try
            {
                if (DetallesFactura == null) return;

                var rol = HttpContext.Session.GetString("RolId");
                var entidadId = HttpContext.Session.GetString("EntidadId");

                var listaTemp = IDetallesFacturaPresentacion!.Consultar();
                DetallesFactura? DetallePermitido = null;

                if (rol == "1")
                {
                    DetallePermitido = listaTemp!.FirstOrDefault(x => x.Id == DetallesFactura!.Id);
                }
                else if (rol == "2" && int.TryParse(entidadId, out int clienteId))
                {
                    var facturasCliente = IFacturasPresentacion!.Consultar()
                        .Where(f => f.Clientes == clienteId)
                        .Select(f => f.Id)
                        .ToList();

                    DetallePermitido = listaTemp!.FirstOrDefault(x =>
                        x.Id == DetallesFactura!.Id && facturasCliente.Contains(x.Facturas));
                }

                if (DetallePermitido == null)
                {
                    ViewData["Mensaje"] = "No tienes permiso para eliminar este detalle.";
                    OnGet();
                    return;
                }

                DetallesFactura = IDetallesFacturaPresentacion!.Eliminar(DetallesFactura!);

                CargarListaFiltrada();
                DetallesFactura = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrarVal(int data)
        {
            IniciarDetallesFactura();
            try
            {
                OnPostBtRefrescar();
                DetallesFactura = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtCerrar()
        {
            IniciarDetallesFactura();
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}