using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class VentasHTMLModel : PageModel
    {
        private IVentasPresentacion? IVentasPresentacion;
        private IClientesPresentacion? IClientesPresentacion;
        private IEmpleadosPresentacion? IEmpleadosPresentacion;
        private IAutosPresentacion? IAutosPresentacion;
        [BindProperty] public List<Ventas>? Lista { get; set; }
        [BindProperty] public List<Clientes>? ListaCliente { get; set; }
        [BindProperty] public List<Empleados>? ListaEmpleado { get; set; }
        [BindProperty] public List<Autos>? ListaAuto { get; set; }
        [BindProperty] public Ventas? Venta { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public VentasHTMLModel()
        {
            IVentasPresentacion = new VentasPresentacion();
            IClientesPresentacion = new ClientesPresentacion();
            IEmpleadosPresentacion = new EmpleadosPresentacion();
            IAutosPresentacion = new AutosPresentacion();
        }

        private void CargarListaFiltrada()
        {
            var rol = HttpContext.Session.GetString("RolId");
            var entidadId = HttpContext.Session.GetString("EntidadId");

            Lista = IVentasPresentacion!.Consultar();

            if (rol == "2" && int.TryParse(entidadId, out int clienteId))
                Lista = Lista!.Where(x => x.Clientes == clienteId).ToList();
            else if (rol == "3" && int.TryParse(entidadId, out int empleadoId))
                Lista = Lista!.Where(x => x.Empleados == empleadoId).ToList();
            else if (rol == "4" && int.TryParse(entidadId, out int duenoId))
            {
                var autosDueno = IAutosPresentacion!.Consultar()
                    .Where(a => a.Duenos.HasValue && a.Duenos.Value == duenoId)
                    .Select(a => a.Id)
                    .ToList();

                Lista = Lista!.Where(x => x.Autos.HasValue && autosDueno.Contains(x.Autos.Value)).ToList();
            }
        }

        public void OnGet()
        {
            try
            {
                CargarListaFiltrada();
                Venta = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public List<Clientes> ObtenerClientes()
            => ListaCliente = IClientesPresentacion!.Consultar();

        public List<Empleados> ObtenerEmpleados()
            => ListaEmpleado = IEmpleadosPresentacion!.Consultar();

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
                Venta = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            var rol = HttpContext.Session.GetString("RolId");

            if (rol == "4")
            {
                ViewData["Mensaje"] = "No tienes permiso para crear ventas.";
                CargarListaFiltrada();
                return;
            }

            Venta = new Ventas();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                var rol = HttpContext.Session.GetString("RolId");
                var entidadId = HttpContext.Session.GetString("EntidadId");

                var listaTemp = IVentasPresentacion!.Consultar();

                if (rol == "1")
                    Venta = listaTemp!.FirstOrDefault(x => x.Id == data);
                else if (rol == "2" && int.TryParse(entidadId, out int clienteId))
                    Venta = listaTemp!.FirstOrDefault(x => x.Id == data && x.Clientes == clienteId);
                else if (rol == "3" && int.TryParse(entidadId, out int empleadoId))
                    Venta = listaTemp!.FirstOrDefault(x => x.Id == data && x.Empleados == empleadoId);
                else if (rol == "4")
                {
                    ViewData["Mensaje"] = "No tienes permiso para modificar ventas.";
                    CargarListaFiltrada();
                    return;
                }

                if (Venta == null)
                    ViewData["Mensaje"] = "No tienes permiso para modificar esta venta.";

                Lista = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Venta == null)
                    return;

                var rol = HttpContext.Session.GetString("RolId");
                var entidadId = HttpContext.Session.GetString("EntidadId");

                if (rol == "4")
                {
                    ViewData["Mensaje"] = "No tienes permiso para guardar ventas.";
                    CargarListaFiltrada();
                    return;
                }

                if (rol == "2" && int.TryParse(entidadId, out int clienteId))
                    Venta.Clientes = clienteId;
                else if (rol == "3" && int.TryParse(entidadId, out int empleadoId))
                    Venta.Empleados = empleadoId;

               
                if ((Venta.Clientes ?? 0) == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar un cliente.";
                    return;
                }

               
                if ((Venta.Empleados ?? 0) == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar un empleado.";
                    return;
                }

               
                if ((Venta.Autos ?? 0) == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar un auto.";
                    return;
                }

                if (Venta.Id == 0)
                    Venta = IVentasPresentacion!.Guardar(Venta!);
                else
                    Venta = IVentasPresentacion!.Modificar(Venta!);

                if (Venta.Id == 0)
                {
                    ViewData["Mensaje"] = "No fue posible guardar la venta.";
                    return;
                }

                ViewData["Mensaje"] = "Venta guardada correctamente.";

                CargarListaFiltrada();
                Venta = null;
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
                if (Venta == null) return;

                var rol = HttpContext.Session.GetString("RolId");
                var entidadId = HttpContext.Session.GetString("EntidadId");

                if (rol != "1")
                {
                    ViewData["Mensaje"] = "No tienes permiso para eliminar ventas.";
                    OnGet();
                    return;
                }

                var listaTemp = IVentasPresentacion!.Consultar();
                var VentaPermitida = listaTemp!.FirstOrDefault(x => x.Id == Venta!.Id);

                if (VentaPermitida == null)
                {
                    ViewData["Mensaje"] = "Venta no encontrada.";
                    OnGet();
                    return;
                }

                Venta = IVentasPresentacion!.Eliminar(Venta!);

                CargarListaFiltrada();
                Venta = null;
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
                if (rol != "1")
                {
                    ViewData["Mensaje"] = "No tienes permiso para eliminar ventas.";
                    CargarListaFiltrada();
                    return;
                }

                OnPostBtRefrescar();
                Venta = Lista!.FirstOrDefault(x => x.Id == data);
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
