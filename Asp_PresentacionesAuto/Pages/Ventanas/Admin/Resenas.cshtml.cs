using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class ResenasHTMLModel : PageModel
    {
        private IResenasPresentacion? IResenasPresentacion;
        [BindProperty] public List<Resenas>? Lista { get; set; }
        [BindProperty] public Resenas? Resena { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public ResenasHTMLModel()
        {
          // IResenasPresentacion = new  ResenasPresentacion();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (IResenasPresentacion == null)
                    return;
                Lista = IResenasPresentacion.Consultar();
                Resena = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Resena = new Resenas();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Resena = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Resena == null)
                    return;
                if (Resena.Id == 0)
                    Resena = IResenasPresentacion!.Guardar(Resena!);
                else
                    Resena = IResenasPresentacion!.Modificar(Resena!);
                if (Resena.Id == 0)
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
                if (Resena == null)
                    return;
                Resena = IResenasPresentacion!.Eliminar(Resena!);
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
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}