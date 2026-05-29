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

        public List<Talleres> ObtenerTalleres()
        {
            return ListaTaller = ITalleresPresentacion!.Consultar();
        }

        private void CargarListaFiltrada()
        {
            var rol = HttpContext.Session.GetString("RolId");
            var entidadId = HttpContext.Session.GetString("EntidadId");

            Lista = IMantenimientosPresentacion!.Consultar();

            // Empleado ve todos los mantenimientos sin filtro
            // Dueño solo ve mantenimientos de sus autos
            if (rol == "4" && int.TryParse(entidadId, out int duenoId))
            {
                var autosDueno = IAutosPresentacion!.Consultar()
                    .Where(a => a.Duenos.HasValue && a.Duenos.Value == duenoId)
                    .Select(a => a.Id)
                    .ToList();

                Lista = Lista!.Where(x => autosDueno.Contains(x.Autos ?? 0)).ToList();
            }
        }

        public void OnGet()
        {
            try
            {
                CargarListaFiltrada();
                Mantenimiento = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public List<Autos> ObtenerAutos()
        {
            var rol = HttpContext.Session.GetString("RolId");
            var entidadId = HttpContext.Session.GetString("EntidadId");

            var todos = IAutosPresentacion!.Consultar();

            if (rol == "4" && int.TryParse(entidadId, out int duenoId))
                return ListaAuto = todos.Where(a => a.Duenos.HasValue && a.Duenos.Value == duenoId).ToList();

            return ListaAuto = todos;
        }


        public void OnPostBtRefrescar()
        {
            try
            {
                CargarListaFiltrada();
                Mantenimiento = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            var rol = HttpContext.Session.GetString("RolId");

            // Solo Admin y Empleado pueden crear
            if (rol != "1" && rol != "3")
            {
                ViewData["Mensaje"] = "No tienes permiso para crear mantenimientos.";
                CargarListaFiltrada();
                return;
            }

            Mantenimiento = new Mantenimientos();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                var rol = HttpContext.Session.GetString("RolId");
                var entidadId = HttpContext.Session.GetString("EntidadId");

                var listaTemp = IMantenimientosPresentacion!.Consultar();

                // Admin y Empleado pueden modificar cualquiera
                if (rol == "1" || rol == "3")
                {
                    Mantenimiento = listaTemp!.FirstOrDefault(x => x.Id == data);
                }
                // Dueño no puede modificar
                else
                {
                    ViewData["Mensaje"] = "No tienes permiso para modificar mantenimientos.";
                    CargarListaFiltrada();
                    return;
                }

                if (Mantenimiento == null)
                    ViewData["Mensaje"] = "Mantenimiento no encontrado.";

                Lista = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Mantenimiento == null)
                    return;

                var rol = HttpContext.Session.GetString("RolId");

                // Solo Admin y Empleado pueden guardar
                if (rol != "1" && rol != "3")
                {
                    ViewData["Mensaje"] = "No tienes permiso para guardar mantenimientos.";
                    CargarListaFiltrada();
                    return;
                }
                
                if ((Mantenimiento.Autos ?? 0) == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar un auto.";
                    return;
                }

                if ((Mantenimiento.Talleres ?? 0) == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar un taller.";
                    return;
                }

                if (Mantenimiento.Id == 0)
                    Mantenimiento = IMantenimientosPresentacion!.Guardar(Mantenimiento!);
                else
                    Mantenimiento = IMantenimientosPresentacion!.Modificar(Mantenimiento!);

                if (Mantenimiento.Id == 0)
                {
                    ViewData["Mensaje"] = "No fue posible guardar el mantenimiento.";
                    return;
                }

                ViewData["Mensaje"] = "Mantenimiento guardado correctamente.";

                CargarListaFiltrada();
                Mantenimiento = null;
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
            try
            {
                if (Mantenimiento == null) return;

                var rol = HttpContext.Session.GetString("RolId");

                // Solo Admin puede borrar
                if (rol != "1")
                {
                    ViewData["Mensaje"] = "No tienes permiso para eliminar mantenimientos.";
                    OnGet();
                    return;
                }

                Mantenimiento = IMantenimientosPresentacion!.Eliminar(Mantenimiento!);

                CargarListaFiltrada();
                Mantenimiento = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                var rol = HttpContext.Session.GetString("RolId");

                // Solo Admin ve el popup de borrar
                if (rol != "1")
                {
                    ViewData["Mensaje"] = "No tienes permiso para eliminar mantenimientos.";
                    CargarListaFiltrada();
                    return;
                }

                OnPostBtRefrescar();
                Mantenimiento = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtCerrar()
        {
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}