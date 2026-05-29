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
          IUsuariosPresentacion = new  UsuariosPresentacion();
          IRolesPresentacion = new RolesPresentacion();
          IClientesPresentacion = new ClientesPresentacion();
          IEmpleadosPresentacion = new EmpleadosPresentacion();
          IDuenosPresentacion = new DuenosPresentacion();
        }

        public void OnGet()
        {
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
            Usuario = new Usuarios();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
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
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}