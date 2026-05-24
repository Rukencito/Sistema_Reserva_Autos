
using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Pagos_Razor.Pages
{
    public class PagosHTMLModel : PageModel
    {
        private IPagosPresentacion? IPagosPresentacion;
        [BindProperty] public List<Pagos>? Lista { get; set; }
        [BindProperty] public Pagos? Pago { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public PagosHTMLModel()
        {
          // IPagosPresentacion = new  PagosPresentacion();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (IPagosPresentacion == null)
                    return;
                Lista = IPagosPresentacion.Consultar();
                Pago = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Pago = new Pagos();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Pago = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Pago == null)
                    return;
                if (Pago.Id == 0)
                    Pago = IPagosPresentacion!.Guardar(Pago!);
                else
                    Pago = IPagosPresentacion!.Modificar(Pago!);
                if (Pago.Id == 0)
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
                if (Pago == null)
                    return;
                Pago = IPagosPresentacion!.Eliminar(Pago!);
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
                Pago = Lista!.FirstOrDefault(x => x.Id == data);
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