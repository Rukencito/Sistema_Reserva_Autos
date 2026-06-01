using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class RolesHTMLModel : PageModel
    {
        private IRolesPresentacion? IRolesPresentacion;
        [BindProperty] public List<Roles>? Lista { get; set; }
        [BindProperty] public Roles? Rol { get; set; }
        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public bool TieneError { get; set; }

        public RolesHTMLModel()
        {
        }
        private void IniciarRoles()
        {
            var correo = HttpContext.Session.GetString("Usuario") ?? "Sistema";
            IRolesPresentacion = new RolesPresentacion(correo);
        }

        public void OnGet()
        {
            IniciarRoles();
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            IniciarRoles();
            try
            {
                if (IRolesPresentacion == null)
                    return;
                Lista = IRolesPresentacion.Consultar();
                Rol = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            IniciarRoles();
            Rol = new Roles();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            IniciarRoles();
            try
            {
                OnPostBtRefrescar();
                Rol = Lista!.FirstOrDefault(x => x.Id == data);
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
            IniciarRoles();
            try
            {
                if (Rol == null)
                    return;
                if (Rol.Id == 0)
                    Rol = IRolesPresentacion!.Guardar(Rol!);
                else
                    Rol = IRolesPresentacion!.Modificar(Rol!);
                if (Rol.Id == 0)
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
            IniciarRoles();
            try
            {
                if (Rol == null)
                    return;
                Rol = IRolesPresentacion!.Eliminar(Rol!);
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrarVal(int data)
        {
            IniciarRoles();
            try
            {
                OnPostBtRefrescar();
                Rol = Lista!.FirstOrDefault(x => x.Id == data);
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
            IniciarRoles();
            if (TieneError)
            {
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