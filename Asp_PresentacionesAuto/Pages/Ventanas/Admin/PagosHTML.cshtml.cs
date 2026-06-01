using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class PagosHTMLModel : PageModel
    {
        private IPagosPresentacion? IPagosPresentacion;
        private IFacturasPresentacion? IFacturasPresentacion;
        [BindProperty] public List<Pagos>? Lista { get; set; }
        [BindProperty] public List<Facturas>? ListaFactura { get; set; }
        [BindProperty] public Pagos? Pago { get; set; }
        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public bool TieneError { get; set; }

        public PagosHTMLModel()
        {
        }
        private void IniciarPagos()
        {
            var correo = HttpContext.Session.GetString("Usuario") ?? "Sistema";
            IPagosPresentacion = new PagosPresentacion(correo);
            IFacturasPresentacion = new FacturasPresentacion(correo);
        }

        public List<Facturas> ObtenerFacturas()
        {
            return ListaFactura = IFacturasPresentacion!.Consultar();
        }

        private void CargarListaFiltrada()
        {
            var rol = HttpContext.Session.GetString("RolId");
            var entidadId = HttpContext.Session.GetString("EntidadId");

            Lista = IPagosPresentacion!.Consultar();

            if (rol == "2" && int.TryParse(entidadId, out int clienteId))
            {
                var facturasCliente = IFacturasPresentacion!.Consultar()
                    .Where(f => f.Clientes == clienteId)
                    .Select(f => f.Id)
                    .ToList();

                Lista = Lista!.Where(x => facturasCliente.Contains(x.Facturas!.Value)).ToList();
            }
        }

        public void OnGet()
        {
            IniciarPagos();
            try
            {
                CargarListaFiltrada();
                Pago = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtRefrescar()
        {
            IniciarPagos();
            try
            {
                CargarListaFiltrada();
                Pago = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }


        public void OnPostBtNuevo()
        {
            IniciarPagos();
            Pago = new Pagos();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            IniciarPagos();
            try
            {
                var rol = HttpContext.Session.GetString("RolId");
                var entidadId = HttpContext.Session.GetString("EntidadId");

                var listaTemp = IPagosPresentacion!.Consultar();

                if (rol == "1")
                {
                    Pago = listaTemp!.FirstOrDefault(x => x.Id == data);
                }
                else if (rol == "2" && int.TryParse(entidadId, out int clienteId))
                {
                    var facturasCliente = IFacturasPresentacion!.Consultar()
                        .Where(f => f.Clientes == clienteId)
                        .Select(f => f.Id)
                        .ToList();

                    Pago = listaTemp!.FirstOrDefault(x =>
                        x.Id == data && facturasCliente.Contains(x.Facturas!.Value));
                }

                if (Pago == null)
                    ViewData["Mensaje"] = "No tienes permiso para modificar este pago.";

                Lista = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtGuardar()
        {
            IniciarPagos();
            try
            {
                if (Pago == null)
                    return;

                if ((Pago.Facturas ?? 0) == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar una factura.";
                    return;
                }

                if (Pago.Id == 0)
                    Pago = IPagosPresentacion!.Guardar(Pago!);
                else
                    Pago = IPagosPresentacion!.Modificar(Pago!);

                if (Pago.Id == 0)
                {
                    ViewData["Mensaje"] = "No fue posible guardar el pago.";
                    return;
                }

                ViewData["Mensaje"] = "Pago guardado correctamente.";

                CargarListaFiltrada();
                Pago = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                Exception errorReal = ex;

                while (errorReal.InnerException != null)
                    errorReal = errorReal.InnerException;

                ViewData["Mensaje"] = errorReal.Message;

                CargarListaFiltrada();
            }
        }

        public void OnPostBtBorrar()
        {
            IniciarPagos();
            try
            {
                if (Pago == null) return;

                var rol = HttpContext.Session.GetString("RolId");
                var entidadId = HttpContext.Session.GetString("EntidadId");

                var listaTemp = IPagosPresentacion!.Consultar();
                Pagos? PagoPermitido = null;

                if (rol == "1")
                {
                    PagoPermitido = listaTemp!.FirstOrDefault(x => x.Id == Pago!.Id);
                }
                else if (rol == "2" && int.TryParse(entidadId, out int clienteId))
                {
                    var facturasCliente = IFacturasPresentacion!.Consultar()
                        .Where(f => f.Clientes == clienteId)
                        .Select(f => f.Id)
                        .ToList();

                    PagoPermitido = listaTemp!.FirstOrDefault(x =>
                        x.Id == Pago!.Id && facturasCliente.Contains(x.Facturas!.Value));
                }

                if (PagoPermitido == null)
                {
                    ViewData["Mensaje"] = "No tienes permiso para eliminar este pago.";
                    OnGet();
                    return;
                }

                Pago = IPagosPresentacion!.Eliminar(Pago!);

                CargarListaFiltrada();
                Pago = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrarVal(int data)
        {
            IniciarPagos();
            try
            {
                OnPostBtRefrescar();
                Pago = Lista!.FirstOrDefault(x => x.Id == data);
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
            IniciarPagos();
            if (TieneError)
            {
                CargarListaFiltrada();
                Lista = null;
                Borrando = false;
                TieneError = false;
                ModelState.Clear();
            }
            else
            {
                OnPostBtRefrescar();
                Borrando = false;
            }
        }
    }
}