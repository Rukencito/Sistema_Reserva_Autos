
using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Lib_Presentacion_Autos.Implementaciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class GarantiasHTMLModel : PageModel
    {
        private IGarantiasPresentacion? IGarantiasPresentacion;
        [BindProperty] public List<Garantias>? Lista { get; set; }
        [BindProperty] public Garantias? Garantia { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public GarantiasHTMLModel()
        {
            IGarantiasPresentacion = new GarantiasPresentacion();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (IGarantiasPresentacion == null)
                    return;
                Lista = IGarantiasPresentacion.Consultar();
                Garantia = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Garantia = new Garantias();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Garantia = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Garantia == null)
                    return;
                if (Garantia.Id == 0)
                    Garantia = IGarantiasPresentacion!.Guardar(Garantia!);
                else
                    Garantia = IGarantiasPresentacion!.Modificar(Garantia!);
                if (Garantia.Id == 0)
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
                if (Garantia == null)
                    return;
                Garantia = IGarantiasPresentacion!.Eliminar(Garantia!);
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
                Garantia = Lista!.FirstOrDefault(x => x.Id == data);
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