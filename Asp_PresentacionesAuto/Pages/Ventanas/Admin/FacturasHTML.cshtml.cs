
using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Lib_Presentacion_Autos.Implementaciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class FacturasHTMLModel : PageModel
    {
        private IFacturasPresentacion? IFacturasPresentacion;
        private IClientesPresentacion? IClientesPresentacion;
        [BindProperty] public List<Facturas>? Lista { get; set; }
        [BindProperty] public List<Clientes>? ListaCliente { get; set; }
        [BindProperty] public Facturas? Factura { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public FacturasHTMLModel()
        {
        }
        private void IniciarFacturas()
        {
            var correo = HttpContext.Session.GetString("Usuario") ?? "Sistema";
            IFacturasPresentacion = new FacturasPresentacion(correo);
            IClientesPresentacion = new ClientesPresentacion(correo);

        }

        public List<Clientes> ObtenerClientes()
        {
            return ListaCliente = IClientesPresentacion!.Consultar();
        }

        private void CargarListaFiltrada()
        {
            var rol = HttpContext.Session.GetString("RolId");
            var usuarioId = HttpContext.Session.GetString("EntidadId");

            Lista = IFacturasPresentacion!.Consultar();

            if (rol == "2" && int.TryParse(usuarioId, out int clienteId))
                Lista = Lista!.Where(x => x.Clientes == clienteId).ToList();
        }

        public void OnGet()
        {
            IniciarFacturas();
            try
            {
                CargarListaFiltrada();
                Factura = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }



        public void OnPostBtRefrescar()
        {
            IniciarFacturas();
            try
            {
                CargarListaFiltrada();
                Factura = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            IniciarFacturas();
            Factura = new Facturas();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            IniciarFacturas();
            try
            {
                var rol = HttpContext.Session.GetString("RolId");
                var usuarioId = HttpContext.Session.GetString("EntidadId");

                Lista = IFacturasPresentacion!.Consultar();

                //Admin
                if (rol == "1")
                {
                    Factura = Lista!.FirstOrDefault(x => x.Id == data);
                }

                //Cliente
                else if (rol == "2")
                {
                    Factura = Lista!.FirstOrDefault(x =>
                    x.Id == data && x.Clientes == int.Parse(usuarioId!));
                }

                if (Factura == null)
                {
                    ViewData["Mensaje"] = "No tienes permiso para modificar este Factura.";
                }

                Lista = null;
                Borrando = false;
            }

            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtGuardar()
        {
            IniciarFacturas();
            try
            {
                if (Factura == null) return;

                var rol = HttpContext.Session.GetString("RolId");
                var usuarioId = HttpContext.Session.GetString("EntidadId");

                // Si es cliente o empleado, forzar su propio ID al guardar
                if (rol == "2" && int.TryParse(usuarioId, out int clienteId))
                    Factura.Clientes = clienteId;

                if (Factura.Id == 0)
                    Factura = IFacturasPresentacion!.Guardar(Factura!);
                else
                    Factura = IFacturasPresentacion!.Modificar(Factura!);

                if (Factura.Id == 0) return;

                CargarListaFiltrada();
                Factura = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            IniciarFacturas();
            try
            {
                var rol = HttpContext.Session.GetString("RolId");
                var usuarioId = HttpContext.Session.GetString("EntidadId");

                var Lista = IFacturasPresentacion!.Consultar();

                Facturas? FacturaPermitido = null;

                //Admin
                if (rol == "1")
                {
                    FacturaPermitido = Lista!.FirstOrDefault(x => x.Id == Factura!.Id);
                }

                //Cliente
                else if (rol == "2")
                {
                    FacturaPermitido = Lista!.FirstOrDefault(x =>
                    x.Id == Factura!.Id && x.Clientes.ToString() == usuarioId!);
                }

                if (Factura == null)
                {
                    ViewData["Mensaje"] = "No tienes permiso para modificar este Factura.";
                }

                if (FacturaPermitido == null)
                {
                    ViewData["Mensaje"] = "No tienes permiso para eliminar este Factura.";
                    OnGet();
                    return;
                }

                Factura = IFacturasPresentacion!.Eliminar(Factura!);

                OnGet();

                CargarListaFiltrada();
                Lista = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrarVal(int data)
        {
            IniciarFacturas();
            try
            {
                OnPostBtRefrescar();
                Factura = Lista!.FirstOrDefault(x => x.Id == data);
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
            IniciarFacturas();
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}