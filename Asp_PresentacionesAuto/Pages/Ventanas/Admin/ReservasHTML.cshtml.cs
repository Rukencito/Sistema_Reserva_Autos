using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class ReservasHTMLModel : PageModel
    {
        private IReservasPresentacion? IReservasPresentacion;
        private IAutosPresentacion? IAutosPresentacion;
        private IClientesPresentacion? IClientesPresentacion;

        [BindProperty] public List<Reservas>? Lista { get; set; }
        [BindProperty] public List<Autos>? ListaAuto { get; set; }
        [BindProperty] public List<Clientes>? ListaCliente { get; set; }
        [BindProperty] public Reservas? Reserva { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public ReservasHTMLModel()
        {
        }
        private void IniciarReservas()
        {
            var correo = HttpContext.Session.GetString("Usuario") ?? "Sistema";
            IReservasPresentacion = new ReservasPresentacion(correo);
            IAutosPresentacion = new AutosPresentacion(correo);
            IClientesPresentacion = new ClientesPresentacion(correo);
        }

        public List<Autos> ObtenerAutos()
        {
            return ListaAuto = IAutosPresentacion!.Consultar();
        }
        public List<Clientes> ObtenerClientes()
        {
            return ListaCliente = IClientesPresentacion!.Consultar();
        }

        private void CargarListaFiltrada()
        {
            var rol = HttpContext.Session.GetString("RolId");
            var usuarioId = HttpContext.Session.GetString("EntidadId");

            Lista = IReservasPresentacion!.Consultar();

            if (rol == "2" && int.TryParse(usuarioId, out int clienteId))
                Lista = Lista!.Where(x => x.Clientes == clienteId).ToList();
        }


        public void OnGet()
        {
            IniciarReservas();
            try
            {
                CargarListaFiltrada();
                Reserva = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtRefrescar()
        {
            IniciarReservas();
            try
            {
                CargarListaFiltrada();
                Reserva = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            IniciarReservas();
            Reserva = new Reservas();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            IniciarReservas();
            try
            {
                var rol = HttpContext.Session.GetString("RolId");
                var usuarioId = HttpContext.Session.GetString("EntidadId");

                Lista = IReservasPresentacion!.Consultar();

                //Admin
                if (rol == "1")
                {
                    Reserva = Lista!.FirstOrDefault(x => x.Id == data);
                }

                //Cliente
                else if (rol == "2")
                {
                    Reserva = Lista!.FirstOrDefault(x =>
                    x.Id == data && x.Clientes == int.Parse(usuarioId!));
                }

                if (Reserva == null)
                {
                    ViewData["Mensaje"] = "No tienes permiso para modificar este Reserva.";
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
            IniciarReservas();
            try
            {
                if (Reserva == null)
                    return;

                var rol = HttpContext.Session.GetString("RolId");
                var usuarioId = HttpContext.Session.GetString("EntidadId");

                if (rol == "2" && int.TryParse(usuarioId, out int clienteId))
                    Reserva.Clientes = clienteId;

                
                if ((Reserva.Clientes ?? 0) == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar un cliente.";
                    return;
                }

                
                if ((Reserva.Autos ?? 0) == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar un auto.";
                    return;
                }

                if (Reserva.Id == 0)
                    Reserva = IReservasPresentacion!.Guardar(Reserva!);
                else
                    Reserva = IReservasPresentacion!.Modificar(Reserva!);

                if (Reserva.Id == 0)
                {
                    ViewData["Mensaje"] = "No fue posible guardar la reserva.";
                    return;
                }

                ViewData["Mensaje"] = "Reserva guardada correctamente.";

                CargarListaFiltrada();
                Reserva = null;
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
            IniciarReservas();
            try
            {
                var rol = HttpContext.Session.GetString("RolId");
                var usuarioId = HttpContext.Session.GetString("EntidadId");

                var Lista = IReservasPresentacion!.Consultar();

                Reservas? ReservaPermitido = null;

                //Admin
                if (rol == "1")
                {
                    ReservaPermitido = Lista!.FirstOrDefault(x => x.Id == Reserva!.Id);
                }

                //Cliente
                else if (rol == "2")
                {
                    ReservaPermitido = Lista!.FirstOrDefault(x =>
                    x.Id == Reserva!.Id && x.Clientes.ToString() == usuarioId!);
                }

                if (Reserva == null)
                {
                    ViewData["Mensaje"] = "No tienes permiso para modificar este Reserva.";
                }

                if (ReservaPermitido == null)
                {
                    ViewData["Mensaje"] = "No tienes permiso para eliminar este Reserva.";
                    OnGet();
                    return;
                }

                Reserva = IReservasPresentacion!.Eliminar(Reserva!);

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
            IniciarReservas();
            try
            {
                OnPostBtRefrescar();
                Reserva = Lista!.FirstOrDefault(x => x.Id == data);
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
            IniciarReservas();
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}