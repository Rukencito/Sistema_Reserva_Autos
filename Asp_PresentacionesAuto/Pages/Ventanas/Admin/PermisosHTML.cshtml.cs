using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class PermisosHTMLModel : PageModel
    {
        private IPermisosPresentacion? IPermisosPresentacion;
        private IRolesPresentacion? IRolesPresentacion;
        [BindProperty] public List<Permisos>? Lista { get; set; }
        [BindProperty] public List<Roles>? ListaRoles { get; set; }
        [BindProperty] public Permisos? Permiso { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public PermisosHTMLModel()
        {
        }
        private void IniciarPermisos()
        {
            var correo = HttpContext.Session.GetString("Usuario") ?? "Sistema";
            IPermisosPresentacion = new PermisosPresentacion(correo);
            IRolesPresentacion = new RolesPresentacion(correo);
        }

        public List<Roles> ObtenerRoles()
        {
            return ListaRoles = IRolesPresentacion!.Consultar();
        }

        public void OnGet()
        {
            IniciarPermisos();
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            IniciarPermisos();
            try
            {
                if (IPermisosPresentacion == null)
                    return;
                Lista = IPermisosPresentacion.Consultar();
                Permiso = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            IniciarPermisos();
            Permiso = new Permisos();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            IniciarPermisos();
            try
            {
                OnPostBtRefrescar();
                Permiso = Lista!.FirstOrDefault(x => x.Id == data);
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
            IniciarPermisos();
            try
            {
                if (Permiso == null)
                    return;
                if (Permiso.Roles == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar un rol.";
                    return;
                }
                if (Permiso.Id == 0)
                    Permiso = IPermisosPresentacion!.Guardar(Permiso!);
                else
                    Permiso = IPermisosPresentacion!.Modificar(Permiso!);
                if (Permiso.Id == 0)
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
            IniciarPermisos();
            try
            {
                if (Permiso == null)
                    return;
                Permiso = IPermisosPresentacion!.Eliminar(Permiso!);
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrarVal(int data)
        {
            IniciarPermisos();
            try
            {
                OnPostBtRefrescar();
                Permiso = Lista!.FirstOrDefault(x => x.Id == data);
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
            IniciarPermisos();
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}
