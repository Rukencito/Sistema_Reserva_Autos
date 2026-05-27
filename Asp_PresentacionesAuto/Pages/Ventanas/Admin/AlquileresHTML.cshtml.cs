using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class AlquileresHTMLModel : PageModel
    {
        private IAlquileresPresentacion? IAlquileresPresentacion;
        [BindProperty] public List<Alquileres>? Lista { get; set; }
        [BindProperty] public Alquileres? Alquiler { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public AlquileresHTMLModel()
        {
            IAlquileresPresentacion = new AlquileresPresentacion();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (IAlquileresPresentacion == null)
                    return;
                Lista = IAlquileresPresentacion.Consultar();
                Alquiler = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Alquiler = new Alquileres();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Alquiler = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Alquiler == null)
                    return;
                if (Alquiler.Id == 0)
                    Alquiler = IAlquileresPresentacion!.Guardar(Alquiler!);
                else
                    Alquiler = IAlquileresPresentacion!.Modificar(Alquiler!);
                if (Alquiler.Id == 0)
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
                if (Alquiler == null)
                    return;
                Alquiler = IAlquileresPresentacion!.Eliminar(Alquiler!);
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
                Alquiler = Lista!.FirstOrDefault(x => x.Id == data);
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