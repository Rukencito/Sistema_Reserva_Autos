using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class MantenimientosHTMLModel : PageModel
    {
        private IMantenimientosPresentacion? IMantenimientosPresentacion;
        private IAutosPresentacion? IAutosPresentacion;
        private ITalleresPresentacion? ITalleresPresentacion;
        [BindProperty] public List<Mantenimientos>? Lista { get; set; }
        [BindProperty] public List<Autos>? ListaAuto { get; set; }
        [BindProperty] public List<Talleres>? ListaTaller { get; set; }
        [BindProperty] public Mantenimientos? Mantenimiento { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public MantenimientosHTMLModel()
        {
          IMantenimientosPresentacion = new  MantenimientosPresentacion();
            IAutosPresentacion = new AutosPresentacion();
            ITalleresPresentacion = new TalleresPresentacion();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public List<Autos> ObtenerAutos()
        {
            return ListaAuto = IAutosPresentacion!.Consultar();
        }

        public List<Talleres> ObtenerTalleres()
        {
            return ListaTaller = ITalleresPresentacion!.Consultar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (IMantenimientosPresentacion == null)
                    return;
                Lista = IMantenimientosPresentacion.Consultar();
                Mantenimiento = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Mantenimiento = new Mantenimientos();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Mantenimiento = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Mantenimiento == null)
                    return;
                if (Mantenimiento.Id == 0)
                    Mantenimiento = IMantenimientosPresentacion!.Guardar(Mantenimiento!);
                else
                    Mantenimiento = IMantenimientosPresentacion!.Modificar(Mantenimiento!);
                if (Mantenimiento.Id == 0)
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
                if (Mantenimiento == null)
                    return;
                Mantenimiento = IMantenimientosPresentacion!.Eliminar(Mantenimiento!);
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
                Mantenimiento = Lista!.FirstOrDefault(x => x.Id == data);
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