
using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Lib_Presentacion_Autos.Implementaciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Facturas_Razor.Pages
{
    public class FacturasHTMLModel : PageModel
    {
        private IFacturasPresentacion? IFacturasPresentacion;
        [BindProperty] public List<Facturas>? Lista { get; set; }
        [BindProperty] public Facturas? Factura { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public FacturasHTMLModel()
        {
            IFacturasPresentacion = new FacturasPresentacion();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (IFacturasPresentacion == null)
                    return;
                Lista = IFacturasPresentacion.Consultar();
                Factura = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Factura = new Facturas();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Factura = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Factura == null)
                    return;
                if (Factura.Id == 0)
                    Factura = IFacturasPresentacion!.Guardar(Factura!);
                else
                    Factura = IFacturasPresentacion!.Modificar(Factura!);
                if (Factura.Id == 0)
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
                if (Factura == null)
                    return;
                Factura = IFacturasPresentacion!.Eliminar(Factura!);
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
                Factura = Lista!.FirstOrDefault(x => x.Id == data);
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