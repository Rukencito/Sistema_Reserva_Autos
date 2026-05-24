
using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Devoluciones_Razor.Pages
{
    public class DevolucionesHTMLModel : PageModel
    {
        private IDevolucionesPresentacion? IDevolucionesPresentacion;
        [BindProperty] public List<Devoluciones>? Lista { get; set; }
        [BindProperty] public Devoluciones? Devolucion { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public DevolucionesHTMLModel()
        {
            IDevolucionesPresentacion = new DevolucionesPresentacion();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (IDevolucionesPresentacion == null)
                    return;
                Lista = IDevolucionesPresentacion.Consultar();
                Devolucion = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Devolucion = new Devoluciones();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Devolucion = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Devolucion == null)
                    return;
                if (Devolucion.Id == 0)
                    Devolucion = IDevolucionesPresentacion!.Guardar(Devolucion!);
                else
                    Devolucion = IDevolucionesPresentacion!.Modificar(Devolucion!);
                if (Devolucion.Id == 0)
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
                if (Devolucion == null)
                    return;
                Devolucion = IDevolucionesPresentacion!.Eliminar(Devolucion!);
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
                Devolucion = Lista!.FirstOrDefault(x => x.Id == data);
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