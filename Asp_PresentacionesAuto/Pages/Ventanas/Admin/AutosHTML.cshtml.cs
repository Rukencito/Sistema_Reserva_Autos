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
        [BindProperty] public bool TieneError { get; set; }

        public AutosHTMLModel()
        {

        }

        private void IniciarAutos()
        {
            var correo = HttpContext.Session.GetString("Usuario") ?? "Sistema";
            IAutosPresentacion = new AutosPresentacion(correo);
            IParqueaderosPresentacion = new ParqueaderosPresentacion(correo);
            IDuenosPresentacion = new DuenosPresentacion(correo);
            ISucursalesPresentacion = new SucursalesPresentacion(correo);
            IInventariosPresentacion = new InventariosPresentacion(correo);
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

        private void CargarListaFiltrada()
        {
            var rol = HttpContext.Session.GetString("RolId");
            var entidadId = HttpContext.Session.GetString("EntidadId");

            Lista = IAutosPresentacion!.Consultar();

            if (rol == "4" && int.TryParse(entidadId, out int duenoId))
                Lista = Lista!.Where(x => x.Duenos.HasValue && x.Duenos.Value == duenoId).ToList();
        }

        public void OnGet()
        {
            IniciarAutos();
            try
            {
                CargarListaFiltrada();
                Auto = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtRefrescar()
        {
            IniciarAutos();
            try
            {
                CargarListaFiltrada();
                Auto = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            IniciarAutos();
            Auto = new Autos();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            IniciarAutos();
            try
            {
                var rol = HttpContext.Session.GetString("RolId");
                var entidadId = HttpContext.Session.GetString("EntidadId");

                var listaTemp = IAutosPresentacion!.Consultar();

                if (rol == "1" || rol == "3")
                    Auto = listaTemp!.FirstOrDefault(x => x.Id == data);
                else if (rol == "4" && int.TryParse(entidadId, out int duenoId))
                    Auto = listaTemp!.FirstOrDefault(x => x.Id == data && x.Duenos.HasValue && x.Duenos.Value == duenoId);

                if (Auto == null)
                    ViewData["Mensaje"] = "No tienes permiso para modificar este auto.";

                Lista = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtGuardar()
        {
            IniciarAutos();
            try
            {
                if (Auto == null)
                    return;

                if (!Auto.Duenos.HasValue || Auto.Duenos.Value == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar un dueño.";
                    return;
                }

                if (!Auto.Parqueaderos.HasValue || Auto.Parqueaderos.Value == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar un parqueadero.";
                    return;
                }

                if (!Auto.Sucursales.HasValue || Auto.Sucursales.Value == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar una sucursal.";
                    return;
                }

                if (!Auto.Inventarios.HasValue || Auto.Inventarios.Value == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar un inventario.";
                    return;
                }

                var rol = HttpContext.Session.GetString("RolId");
                var entidadId = HttpContext.Session.GetString("EntidadId");

                if (rol == "4" && int.TryParse(entidadId, out int duenoId))
                    Auto.Duenos = duenoId;

                if (Auto.Id == 0)
                    Auto = IAutosPresentacion!.Guardar(Auto!);
                else
                    Auto = IAutosPresentacion!.Modificar(Auto!);

                if (Auto.Id == 0)
                {
                    ViewData["Mensaje"] = "No fue posible guardar el auto.";
                    return;
                }

                ViewData["Mensaje"] = "Auto guardado correctamente.";

                CargarListaFiltrada();
                Auto = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                Exception errorReal = ex;

                while (errorReal.InnerException != null)
                    errorReal = errorReal.InnerException;

                ViewData["Mensaje"] = errorReal.Message;

                CargarListaFiltrada();
            }
        }

        public void OnPostBtBorrar()
        {
            IniciarAutos();
            try
            {
                if (Auto == null) return;

                var rol = HttpContext.Session.GetString("RolId");
                var entidadId = HttpContext.Session.GetString("EntidadId");

                var listaTemp = IAutosPresentacion!.Consultar();
                Autos? AutoPermitido = null;

                if (rol == "1" || rol == "3")
                    AutoPermitido = listaTemp!.FirstOrDefault(x => x.Id == Auto!.Id);
                else if (rol == "4" && int.TryParse(entidadId, out int duenoId))
                    AutoPermitido = listaTemp!.FirstOrDefault(x => x.Id == Auto!.Id && x.Duenos.HasValue && x.Duenos.Value == duenoId);

                if (AutoPermitido == null)
                {
                    ViewData["Mensaje"] = "No tienes permiso para eliminar este auto.";
                    OnGet();
                    return;
                }

                Auto = IAutosPresentacion!.Eliminar(Auto!);

                CargarListaFiltrada();
                Auto = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                Exception errorReal = ex;
                while (errorReal.InnerException != null)
                    errorReal = errorReal.InnerException;
                ViewData["Mensaje"] = errorReal.Message;
                CargarListaFiltrada(); 
            }
        }

        public void OnPostBtBorrarVal(int data)
        {
            IniciarAutos();
            try
            {
                OnPostBtRefrescar();
                Auto = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex)
            {
                Exception errorReal = ex;
                while (errorReal.InnerException != null)
                    errorReal = errorReal.InnerException;
                ViewData["Mensaje"] = errorReal.Message;
            }
        }

        public void OnPostBtCerrar()
        {
            IniciarAutos();
            if (TieneError)
            {
                CargarListaFiltrada();
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