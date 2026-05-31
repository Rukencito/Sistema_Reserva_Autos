using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class UsuariosHTMLModel : PageModel
    {
        private IUsuariosPresentacion? IUsuariosPresentacion;
        private IRolesPresentacion? IRolesPresentacion;
        private IClientesPresentacion? IClientesPresentacion;
        private IEmpleadosPresentacion? IEmpleadosPresentacion;
        private IDuenosPresentacion? IDuenosPresentacion;
        [BindProperty] public List<Usuarios>? Lista { get; set; }
        [BindProperty] public Usuarios? Usuario { get; set; }

        [BindProperty] public List<Roles>? ListaRoles { get; set; }
        [BindProperty] public List<Clientes>? ListaClientes { get; set; }
        [BindProperty] public List<Empleados>? ListaEmpleados { get; set; }
        [BindProperty] public List<Duenos>? ListaDuenos { get; set; }
        [BindProperty] public bool Borrando { get; set; }


        public UsuariosHTMLModel()
        {
        }
        private void IniciarUsuarios()
        {
            var correo = HttpContext.Session.GetString("Usuario") ?? "Sistema";
            IUsuariosPresentacion = new UsuariosPresentacion(correo);
            IRolesPresentacion = new RolesPresentacion(correo);
            IClientesPresentacion = new ClientesPresentacion(correo);
            IEmpleadosPresentacion = new EmpleadosPresentacion(correo);
            IDuenosPresentacion = new DuenosPresentacion(correo);

        }

        public void OnGet()
        {
            IniciarUsuarios();
            OnPostBtRefrescar();
        }

        public List<Roles> ObtenerRoles()
        {
            return ListaRoles = IRolesPresentacion!.Consultar();
        }

        public List<Clientes> ObtenerClientes()
        {
            return ListaClientes = IClientesPresentacion!.Consultar();
        }

        public List<Empleados> ObtenerEmpleados()
        {
            return ListaEmpleados = IEmpleadosPresentacion!.Consultar();
        }

        public List<Duenos> ObtenerDuenos()
        {
            return ListaDuenos = IDuenosPresentacion!.Consultar();
        }

        public void OnPostBtRefrescar()
        {
            IniciarUsuarios();
            try
            {
                if (IUsuariosPresentacion == null)
                    return;
                Lista = IUsuariosPresentacion.Consultar();
                Usuario = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            IniciarUsuarios();
            Usuario = new Usuarios();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            IniciarUsuarios();
            try
            {
                OnPostBtRefrescar();
                Usuario = Lista!.FirstOrDefault(x => x.Id == data);
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
            IniciarUsuarios();
            try
            {
                if (Usuario == null)
                    return;
                if (Usuario.Id == 0)
                    Usuario = IUsuariosPresentacion!.Guardar(Usuario!);
                else
                    Usuario = IUsuariosPresentacion!.Modificar(Usuario!);
                if (Usuario.Id == 0)
                    return;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrar()
        {
            IniciarUsuarios();
            try
            {
                if (Usuario == null)
                    return;
                Usuario = IUsuariosPresentacion!.Eliminar(Usuario!);
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrarVal(int data)
        {
            IniciarUsuarios();
            try
            {
                OnPostBtRefrescar();
                Usuario = Lista!.FirstOrDefault(x => x.Id == data);
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
            IniciarUsuarios();
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}