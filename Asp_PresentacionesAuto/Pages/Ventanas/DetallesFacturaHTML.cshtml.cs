
using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DetallesFactura_Razor.Pages
{
    public class DetallesFacturaHTMLModel : PageModel
    {
        private IDetallesFacturaPresentacion? IDetallesFacturaPresentacion;
        [BindProperty] public List<DetallesFactura>? Lista { get; set; }
        [BindProperty] public DetallesFactura? DetallesFactura { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public DetallesFacturaHTMLModel()
        {
            IDetallesFacturaPresentacion = new DetallesFacturaPresentacion();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (IDetallesFacturaPresentacion == null)
                    return;
                Lista = IDetallesFacturaPresentacion.Consultar();
                DetallesFactura = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            DetallesFactura = new DetallesFactura();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                DetallesFactura = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (DetallesFactura == null)
                    return;
                if (DetallesFactura.Id == 0)
                    DetallesFactura = IDetallesFacturaPresentacion!.Guardar(DetallesFactura!);
                else
                    DetallesFactura = IDetallesFacturaPresentacion!.Modificar(DetallesFactura!);
                if (DetallesFactura.Id == 0)
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
                if (DetallesFactura == null)
                    return;
                DetallesFactura = IDetallesFacturaPresentacion!.Eliminar(DetallesFactura!);
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
                DetallesFactura = Lista!.FirstOrDefault(x => x.Id == data);
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