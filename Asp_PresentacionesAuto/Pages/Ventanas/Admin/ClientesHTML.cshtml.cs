using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class ClientesHTMLModel : PageModel
    {
        private IClientesPresentacion? IClientesPresentacion;
        [BindProperty] public List<Clientes>? Lista { get; set; }
        [BindProperty] public Clientes? Cliente { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public ClientesHTMLModel()
        {
            IClientesPresentacion = new ClientesPresentacion();
        }

        private void IniciarClientes()
        {
            var correo = HttpContext.Session.GetString("Usuario") ?? "Sistema";
            IClientesPresentacion = new ClientesPresentacion(correo);
        }


        private void CargarListaFiltrada()
        {
            Lista = IClientesPresentacion!.Consultar();
        }

        public void OnGet()
        {
            IniciarClientes();
            try
            {
                CargarListaFiltrada();
                Cliente = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtRefrescar()
        {
            IniciarClientes();
            try
            {
                CargarListaFiltrada();
                Cliente = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            IniciarClientes();
            var rol = HttpContext.Session.GetString("RolId");
            if (rol != "1")
            {
                ViewData["Mensaje"] = "No tienes permiso para crear clientes.";
                CargarListaFiltrada();
                return;
            }
            Cliente = new Clientes();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            IniciarClientes();
            try
            {
                var rol = HttpContext.Session.GetString("RolId");

                if (rol == "1" || rol == "3")
                {
                    var listaTemp = IClientesPresentacion!.Consultar();
                    Cliente = listaTemp!.FirstOrDefault(x => x.Id == data);
                }
                else
                {
                    ViewData["Mensaje"] = "No tienes permiso para modificar clientes.";
                    CargarListaFiltrada();
                    return;
                }

                if (Cliente == null)
                    ViewData["Mensaje"] = "Cliente no encontrado.";

                Lista = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtGuardar()
        {
            IniciarClientes();
            try
            {
                if (Cliente == null)
                    return;

                var rol = HttpContext.Session.GetString("RolId");

                if (rol != "1" && rol != "3")
                {
                    ViewData["Mensaje"] = "No tienes permiso para guardar clientes.";
                    CargarListaFiltrada();
                    return;
                }

                if (Cliente.Id == 0)
                    Cliente = IClientesPresentacion!.Guardar(Cliente!);
                else
                    Cliente = IClientesPresentacion!.Modificar(Cliente!);

                if (Cliente.Id == 0)
                {
                    ViewData["Mensaje"] = "No fue posible guardar el cliente.";
                    return;
                }

                ViewData["Mensaje"] = "Cliente guardado correctamente.";

                CargarListaFiltrada();
                Cliente = null;
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
            IniciarClientes();
            try
            {
                if (Cliente == null) return;

                var rol = HttpContext.Session.GetString("RolId");

                if (rol != "1")
                {
                    ViewData["Mensaje"] = "No tienes permiso para eliminar clientes.";
                    OnGet();
                    return;
                }

                Cliente = IClientesPresentacion!.Eliminar(Cliente!);

                CargarListaFiltrada();
                Cliente = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrarVal(int data)
        {
            IniciarClientes();
            try
            {
                var rol = HttpContext.Session.GetString("RolId");

                if (rol != "1")
                {
                    ViewData["Mensaje"] = "No tienes permiso para eliminar clientes.";
                    CargarListaFiltrada();
                    return;
                }

                OnPostBtRefrescar();
                Cliente = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtCerrar()
        {
            IniciarClientes();
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}