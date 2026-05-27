using Lib_Negocio_Autos.modelo;
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

        public ParqueaderosHTMLModel()
        {
          // IParqueaderosPresentacion = new  ParqueaderosPresentacion();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
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
            Parqueadero = new Parqueaderos();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
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
            try
            {
                if (Parqueadero == null)
                    return;
                if (Parqueadero.Id == 0)
                    Parqueadero = IParqueaderosPresentacion!.Guardar(Parqueadero!);
                else
                    Parqueadero = IParqueaderosPresentacion!.Modificar(Parqueadero!);
                if (Parqueadero.Id == 0)
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
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}