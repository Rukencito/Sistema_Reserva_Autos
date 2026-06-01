using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class ParqueaderosHTMLModel : PageModel
    {
        private IParqueaderosPresentacion? IParqueaderosPresentacion;
        [BindProperty] public List<Parqueaderos>? Lista { get; set; }
        [BindProperty] public Parqueaderos? Parqueadero { get; set; }
        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public bool TieneError { get; set; }

        public ParqueaderosHTMLModel()
        {
        }
        private void IniciarParqueaderos()
        {
            var correo = HttpContext.Session.GetString("Usuario") ?? "Sistema";
            IParqueaderosPresentacion = new ParqueaderosPresentacion(correo);
        }
        public void OnGet()
        {
            IniciarParqueaderos();
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            IniciarParqueaderos();
            try
            {
                if (IParqueaderosPresentacion == null)
                    return;
                Lista = IParqueaderosPresentacion.Consultar();
                Parqueadero = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            IniciarParqueaderos();
            Parqueadero = new Parqueaderos();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            IniciarParqueaderos();
            try
            {
                OnPostBtRefrescar();
                Parqueadero = Lista!.FirstOrDefault(x => x.Id == data);
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
            IniciarParqueaderos();
            try
            {
                if (Parqueadero == null)
                    return;

                if (Parqueadero.Id == 0)
                    Parqueadero = IParqueaderosPresentacion!.Guardar(Parqueadero!);
                else
                    Parqueadero = IParqueaderosPresentacion!.Modificar(Parqueadero!);

                if (Parqueadero.Id == 0)
                {
                    ViewData["Mensaje"] = "No fue posible guardar el parqueadero.";
                    return;
                }

                ViewData["Mensaje"] = "Parqueadero guardado correctamente.";

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
            IniciarParqueaderos();
            try
            {
                if (Parqueadero == null)
                    return;
                Parqueadero = IParqueaderosPresentacion!.Eliminar(Parqueadero!);
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrarVal(int data)
        {
            IniciarParqueaderos();
            try
            {
                OnPostBtRefrescar();
                Parqueadero = Lista!.FirstOrDefault(x => x.Id == data);
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
            IniciarParqueaderos();
            if (TieneError)
            {
                Lista = null;
                Borrando = false;
                TieneError = false;
                ModelState.Clear();
            }
            else
            {
                OnPostBtRefrescar();
                Borrando = false;
            }
        }
    }
}