using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class ResenasHTMLModel : PageModel
    {
        private IResenasPresentacion? IResenasPresentacion;
        private IClientesPresentacion? IClientesPresentacion;
        [BindProperty] public List<Resenas>? Lista { get; set; }
        [BindProperty] public List<Clientes>? ListaCliente { get; set; }
        [BindProperty] public Resenas? Resena { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public ResenasHTMLModel()
        {
        }
        private void IniciarResenas()
        {
            var correo = HttpContext.Session.GetString("Usuario") ?? "Sistema";
            IResenasPresentacion = new ResenasPresentacion(correo);
            IClientesPresentacion = new ClientesPresentacion();
        }

        public List<Clientes> ObtenerClientes()
        {
            return ListaCliente = IClientesPresentacion!.Consultar();
        }

        private void CargarListaFiltrada()
        {
            var rol = HttpContext.Session.GetString("RolId");
            var usuarioId = HttpContext.Session.GetString("EntidadId");

            Lista = IResenasPresentacion!.Consultar();

            if (rol == "2" && int.TryParse(usuarioId, out int clienteId))
                Lista = Lista!.Where(x => x.Clientes == clienteId).ToList();
        }

        public void OnGet()
        {
            IniciarResenas();
            try
            {
                CargarListaFiltrada();
                Resena = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }


        public void OnPostBtRefrescar()
        {
            IniciarResenas();
            try
            {
                CargarListaFiltrada();
                Resena = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            IniciarResenas();
            Resena = new Resenas();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            IniciarResenas();
            try
            {
                var rol = HttpContext.Session.GetString("RolId");
                var usuarioId = HttpContext.Session.GetString("EntidadId");

                Lista = IResenasPresentacion!.Consultar();

                //Admin
                if (rol == "1")
                {
                    Resena = Lista!.FirstOrDefault(x => x.Id == data);
                }

                //Cliente
                else if (rol == "2")
                {
                    Resena = Lista!.FirstOrDefault(x =>
                    x.Id == data && x.Clientes == int.Parse(usuarioId!));
                }

                if (Resena == null)
                {
                    ViewData["Mensaje"] = "No tienes permiso para modificar este Resena.";
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
            IniciarResenas();
            try
            {
                if (Resena == null)
                    return;

                var rol = HttpContext.Session.GetString("RolId");
                var usuarioId = HttpContext.Session.GetString("EntidadId");

                // Si es cliente, forzar su propio ID
                if (rol == "2" && int.TryParse(usuarioId, out int clienteId))
                    Resena.Clientes = clienteId;

                // Validar FK Cliente
                if (Resena.Clientes == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar un cliente.";
                    return;
                }

                if (Resena.Id == 0)
                    Resena = IResenasPresentacion!.Guardar(Resena!);
                else
                    Resena = IResenasPresentacion!.Modificar(Resena!);

                if (Resena.Id == 0)
                {
                    ViewData["Mensaje"] = "No fue posible guardar la reseña.";
                    return;
                }

                ViewData["Mensaje"] = "Reseña guardada correctamente.";

                CargarListaFiltrada();
                Resena = null;
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
            IniciarResenas();
            try
            {
                var rol = HttpContext.Session.GetString("RolId");
                var usuarioId = HttpContext.Session.GetString("EntidadId");

                var Lista = IResenasPresentacion!.Consultar();

                Resenas? ResenaPermitido = null;

                //Admin
                if (rol == "1")
                {
                    ResenaPermitido = Lista!.FirstOrDefault(x => x.Id == Resena!.Id);
                }

                //Cliente
                else if (rol == "2")
                {
                    ResenaPermitido = Lista!.FirstOrDefault(x =>
                    x.Id == Resena!.Id && x.Clientes.ToString() == usuarioId!);
                }

                if (Resena == null)
                {
                    ViewData["Mensaje"] = "No tienes permiso para modificar este Resena.";
                }

                if (ResenaPermitido == null)
                {
                    ViewData["Mensaje"] = "No tienes permiso para eliminar este Resena.";
                    OnGet();
                    return;
                }

                Resena = IResenasPresentacion!.Eliminar(Resena!);

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
            IniciarResenas();
            try
            {
                OnPostBtRefrescar();
                Resena = Lista!.FirstOrDefault(x => x.Id == data);
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
            IniciarResenas();
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}