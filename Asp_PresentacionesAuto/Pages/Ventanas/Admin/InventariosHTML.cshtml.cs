using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class InventariosHTMLModel : PageModel
    {
        private IInventariosPresentacion? IInventariosPresentacion;
        [BindProperty] public List<Inventarios>? Lista { get; set; }
        [BindProperty] public Inventarios? Inventario { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public InventariosHTMLModel()
        {
        }
        private void IniciarInventarios()
        {
            var correo = HttpContext.Session.GetString("Usuario") ?? "Sistema";
            IInventariosPresentacion = new InventariosPresentacion(correo);
        }

        public void OnGet()
        {
            IniciarInventarios();
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            IniciarInventarios();
            try
            {
                if (IInventariosPresentacion == null)
                    return;
                Lista = IInventariosPresentacion.Consultar();
                Inventario = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            IniciarInventarios();
            Inventario = new Inventarios();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            IniciarInventarios();
            try
            {
                OnPostBtRefrescar();
                Inventario = Lista!.FirstOrDefault(x => x.Id == data);
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
            IniciarInventarios();
            try
            {
                if (Inventario == null)
                    return;

                if (Inventario.Id == 0)
                    Inventario = IInventariosPresentacion!.Guardar(Inventario!);
                else
                    Inventario = IInventariosPresentacion!.Modificar(Inventario!);

                if (Inventario.Id == 0)
                {
                    ViewData["Mensaje"] = "No fue posible guardar el inventario.";
                    return;
                }

                ViewData["Mensaje"] = "Inventario guardado correctamente.";

                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                Exception errorReal = ex;

                while (errorReal.InnerException != null)
                    errorReal = errorReal.InnerException;

                ViewData["Mensaje"] = errorReal.Message;

                OnPostBtRefrescar();
            }
        }

        public void OnPostBtBorrar()
        {
            IniciarInventarios();
            try
            {
                if (Inventario == null)
                    return;
                Inventario = IInventariosPresentacion!.Eliminar(Inventario!);
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrarVal(int data)
        {
            IniciarInventarios();
            try
            {
                OnPostBtRefrescar();
                Inventario = Lista!.FirstOrDefault(x => x.Id == data);
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
            IniciarInventarios();
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}