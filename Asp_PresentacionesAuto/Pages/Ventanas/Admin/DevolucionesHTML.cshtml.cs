using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class DevolucionesHTMLModel : PageModel
    {
        private IDevolucionesPresentacion? IDevolucionesPresentacion;
        private IAlquileresPresentacion? IAlquileresPresentacion;
        [BindProperty] public List<Devoluciones>? Lista { get; set; }
        [BindProperty] public List<Alquileres>? ListaAlquiler { get; set; }
        [BindProperty] public Devoluciones? Devolucion { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public DevolucionesHTMLModel()
        {
            IDevolucionesPresentacion = new DevolucionesPresentacion();
            IAlquileresPresentacion = new AlquileresPresentacion();
        }

        private void CargarListaFiltrada()
        {
            var rol = HttpContext.Session.GetString("RolId");
            var entidadId = HttpContext.Session.GetString("EntidadId");

            Lista = IDevolucionesPresentacion!.Consultar();

            // Empleado solo ve devoluciones de sus alquileres
            if (rol == "3" && int.TryParse(entidadId, out int empleadoId))
            {
                var alquileresEmpleado = IAlquileresPresentacion!.Consultar()
                    .Where(a => a.Empleados == empleadoId)
                    .Select(a => a.Id)
                    .ToList();

                Lista = Lista!.Where(x => alquileresEmpleado.Contains(x.Alquileres)).ToList();
            }
        }

        public void OnGet()
        {
            try
            {
                CargarListaFiltrada();
                Devolucion = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public List<Alquileres> ObtenerAlquileres()
        {
            var rol = HttpContext.Session.GetString("RolId");
            var entidadId = HttpContext.Session.GetString("EntidadId");

            var todos = IAlquileresPresentacion!.Consultar();

            // Empleado solo ve sus alquileres en el select
            if (rol == "3" && int.TryParse(entidadId, out int empleadoId))
                return ListaAlquiler = todos.Where(a => a.Empleados == empleadoId).ToList();

            return ListaAlquiler = todos;
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                CargarListaFiltrada();
                Devolucion = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
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
                var rol = HttpContext.Session.GetString("RolId");
                var entidadId = HttpContext.Session.GetString("EntidadId");

                var listaTemp = IDevolucionesPresentacion!.Consultar();

                if (rol == "1")
                {
                    Devolucion = listaTemp!.FirstOrDefault(x => x.Id == data);
                }
                else if (rol == "3" && int.TryParse(entidadId, out int empleadoId))
                {
                    var alquileresEmpleado = IAlquileresPresentacion!.Consultar()
                        .Where(a => a.Empleados == empleadoId)
                        .Select(a => a.Id)
                        .ToList();

                    Devolucion = listaTemp!.FirstOrDefault(x =>
                        x.Id == data && alquileresEmpleado.Contains(x.Alquileres));
                }

                if (Devolucion == null)
                    ViewData["Mensaje"] = "No tienes permiso para modificar esta devolución.";

                Lista = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Devolucion == null)
                    return;

                if (Devolucion.Alquileres == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar un alquiler.";
                    return;
                }

                if (Devolucion.Id == 0)
                    Devolucion = IDevolucionesPresentacion!.Guardar(Devolucion!);
                else
                    Devolucion = IDevolucionesPresentacion!.Modificar(Devolucion!);

                if (Devolucion.Id == 0)
                {
                    ViewData["Mensaje"] = "No fue posible guardar la devolución.";
                    return;
                }

                ViewData["Mensaje"] = "Devolución guardada correctamente.";

                CargarListaFiltrada();
                Devolucion = null;
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
                if (Devolucion == null) return;

                var rol = HttpContext.Session.GetString("RolId");

                // Solo Admin puede borrar
                if (rol != "1")
                {
                    ViewData["Mensaje"] = "No tienes permiso para eliminar devoluciones.";
                    OnGet();
                    return;
                }

                Devolucion = IDevolucionesPresentacion!.Eliminar(Devolucion!);

                CargarListaFiltrada();
                Devolucion = null;
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
                    ViewData["Mensaje"] = "No tienes permiso para eliminar devoluciones.";
                    CargarListaFiltrada();
                    return;
                }

                OnPostBtRefrescar();
                Devolucion = Lista!.FirstOrDefault(x => x.Id == data);
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