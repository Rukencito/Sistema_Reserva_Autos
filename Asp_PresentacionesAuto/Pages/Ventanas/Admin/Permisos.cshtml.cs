using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class PermisosHTMLModel : PageModel
    {
        private IPermisosPresentacion? IPermisosPresentacion;
        [BindProperty] public List<Permisos>? Lista { get; set; }
        [BindProperty] public Permisos? Permiso { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public PermisosHTMLModel()
        {
          // IPermisosPresentacion = new  PermisosPresentacion();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
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
            Permiso = new Permisos();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
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
            try
            {
                if (Permiso == null)
                    return;
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
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}