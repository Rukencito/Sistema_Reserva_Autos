using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class SegurosHTMLModel : PageModel
    {
        private ISegurosPresentacion? ISegurosPresentacion;
        private IAutosPresentacion? IAutosPresentacion;
        [BindProperty] public List<Seguros>? Lista { get; set; }
        [BindProperty] public List<Autos>? ListaAuto { get; set; }
        [BindProperty] public Seguros? Seguro { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public SegurosHTMLModel()
        {
          ISegurosPresentacion = new  SegurosPresentacion();
            IAutosPresentacion = new AutosPresentacion();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public List<Autos> ObtenerAutos()
        {
            return ListaAuto = IAutosPresentacion!.Consultar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (ISegurosPresentacion == null)
                    return;
                Lista = ISegurosPresentacion.Consultar();
                Seguro = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Seguro = new Seguros();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Seguro = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Seguro == null)
                    return;
                if (Seguro.Id == 0)
                    Seguro = ISegurosPresentacion!.Guardar(Seguro!);
                else
                    Seguro = ISegurosPresentacion!.Modificar(Seguro!);
                if (Seguro.Id == 0)
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
                if (Seguro == null)
                    return;
                Seguro = ISegurosPresentacion!.Eliminar(Seguro!);
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
                Seguro = Lista!.FirstOrDefault(x => x.Id == data);
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