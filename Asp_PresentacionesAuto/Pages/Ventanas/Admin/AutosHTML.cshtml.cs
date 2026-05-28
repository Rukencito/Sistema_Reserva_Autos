using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class AutosHTMLModel : PageModel
    {
        private IAutosPresentacion? IAutosPresentacion;
        private IParqueaderosPresentacion? IParqueaderosPresentacion;
        private IDuenosPresentacion? IDuenosPresentacion;
        private ISucursalesPresentacion? ISucursalesPresentacion;
        private IInventariosPresentacion? IInventariosPresentacion;
        [BindProperty] public List<Autos>? Lista { get; set; }
        [BindProperty] public List<Parqueaderos>? ListaParqueadero { get; set; }
        [BindProperty] public List<Duenos>? ListaDueno { get; set; }
        [BindProperty] public List<Sucursales>? ListaSucursal { get; set; }
        [BindProperty] public List<Inventarios>? ListaInventario { get; set; }
        [BindProperty] public Autos? Auto { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public AutosHTMLModel()
        {
            IAutosPresentacion = new AutosPresentacion();
            IParqueaderosPresentacion = new ParqueaderosPresentacion();
            IDuenosPresentacion = new DuenosPresentacion();
            ISucursalesPresentacion = new SucursalesPresentacion();
            IInventariosPresentacion = new InventariosPresentacion();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }
        public List<Parqueaderos> ObtenerParqueaderos()
        {
            return ListaParqueadero = IParqueaderosPresentacion!.Consultar();
        }

        public List<Duenos> ObtenerDuenos()
        {
            return ListaDueno = IDuenosPresentacion!.Consultar();
        }

        public List<Sucursales> ObtenerSucursales()
        {
            return ListaSucursal = ISucursalesPresentacion!.Consultar();
        }

        public List<Inventarios> ObtenerInventarios()
        {
            return ListaInventario = IInventariosPresentacion!.Consultar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (IAutosPresentacion == null)
                    return;

                Lista = IAutosPresentacion.Consultar();

                Auto = null;

                Borrando = false;

                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Auto = new Autos();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Auto = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Auto == null)
                    return;
                if (Auto.Id == 0)
                    Auto = IAutosPresentacion!.Guardar(Auto!);
                else
                    Auto = IAutosPresentacion!.Modificar(Auto!);
                if (Auto.Id == 0)
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
                if (Auto == null)
                    return;
                Auto = IAutosPresentacion!.Eliminar(Auto!);
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
                Auto = Lista!.FirstOrDefault(x => x.Id == data);
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