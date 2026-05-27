using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class DuenosHTMLModel : PageModel
    {
        private IDuenosPresentacion? IDuenosPresentacion;
        [BindProperty] public List<Duenos>? Lista { get; set; }
        [BindProperty] public Duenos? Dueno { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public DuenosHTMLModel()
        {
            IDuenosPresentacion = new DuenosPresentacion();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (IDuenosPresentacion == null)
                    return;
                Lista = IDuenosPresentacion.Consultar();
                Dueno = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Dueno = new Duenos();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Dueno = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Dueno == null)
                    return;
                if (Dueno.Id == 0)
                    Dueno = IDuenosPresentacion!.Guardar(Dueno!);
                else
                    Dueno = IDuenosPresentacion!.Modificar(Dueno!);
                if (Dueno.Id == 0)
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
                if (Dueno == null)
                    return;
                Dueno = IDuenosPresentacion!.Eliminar(Dueno!);
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
                Dueno = Lista!.FirstOrDefault(x => x.Id == data);
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