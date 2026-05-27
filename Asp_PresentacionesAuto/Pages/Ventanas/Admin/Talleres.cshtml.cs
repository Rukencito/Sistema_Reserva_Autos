using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class TalleresHTMLModel : PageModel
    {
        private ITalleresPresentacion? ITalleresPresentacion;
        [BindProperty] public List<Talleres>? Lista { get; set; }
        [BindProperty] public Talleres? Taller { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public TalleresHTMLModel()
        {
          // ITalleresPresentacion = new  TalleresPresentacion();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (ITalleresPresentacion == null)
                    return;
                Lista = ITalleresPresentacion.Consultar();
                Taller = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Taller = new Talleres();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Taller = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Taller == null)
                    return;
                if (Taller.Id == 0)
                    Taller = ITalleresPresentacion!.Guardar(Taller!);
                else
                    Taller = ITalleresPresentacion!.Modificar(Taller!);
                if (Taller.Id == 0)
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
                if (Taller == null)
                    return;
                Taller = ITalleresPresentacion!.Eliminar(Taller!);
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
                Taller = Lista!.FirstOrDefault(x => x.Id == data);
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