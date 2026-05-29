using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class AlquileresHTMLModel : PageModel
    {
        private IAlquileresPresentacion? IAlquileresPresentacion;
        private IAutosPresentacion? IAutosPresentacion;
        private IClientesPresentacion? IClientesPresentacion;
        private IEmpleadosPresentacion? IEmpleadosPresentacion;
        [BindProperty] public List<Alquileres>? Lista { get; set; }
        [BindProperty] public List<Autos>? ListaAuto { get; set; }
        [BindProperty] public List<Clientes>? ListaCliente { get; set; }
        [BindProperty] public List<Empleados>? ListaEmpleado { get; set; }
        [BindProperty] public Alquileres? Alquiler { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public AlquileresHTMLModel()
        {
            IAlquileresPresentacion = new AlquileresPresentacion();
            IAutosPresentacion= new AutosPresentacion();
            IClientesPresentacion= new  ClientesPresentacion();
            IEmpleadosPresentacion= new  EmpleadosPresentacion();
        }

        public List<Autos> ObtenerAutos()
        {
            return ListaAuto = IAutosPresentacion!.Consultar();
        }
        public List<Clientes> ObtenerClientes()
        {
            return ListaCliente = IClientesPresentacion!.Consultar();
        }

        public List<Empleados> ObtenerEmpleados()
        {
            return ListaEmpleado = IEmpleadosPresentacion!.Consultar();
        }


        private void CargarListaFiltrada()
        {
            var rol = HttpContext.Session.GetString("RolId");
            var usuarioId = HttpContext.Session.GetString("EntidadId");

            Lista = IAlquileresPresentacion!.Consultar();

            if (rol == "2" && int.TryParse(usuarioId, out int clienteId))
                Lista = Lista!.Where(x => x.Clientes == clienteId).ToList();
            else if (rol == "3" && int.TryParse(usuarioId, out int empleadoId))
                Lista = Lista!.Where(x => x.Empleados == empleadoId).ToList();
            else if (rol == "4" && int.TryParse(usuarioId, out int duenoId))
            {
                var autosDueno = IAutosPresentacion!.Consultar()
                    .Where(a => a.Duenos.HasValue && a.Duenos.Value == duenoId)
                    .Select(a => a.Id)
                    .ToList();

                Lista = Lista!.Where(x => autosDueno.Contains(x.Autos)).ToList();
            }
        }

        public void OnGet()
        {
            try
            {
                CargarListaFiltrada();
                Alquiler = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                CargarListaFiltrada();
                Alquiler = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
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
                var rol = HttpContext.Session.GetString("RolId");
                var usuarioId = HttpContext.Session.GetString("EntidadId");

                var listaTemp = IAlquileresPresentacion!.Consultar();

                if (rol == "1")
                    Alquiler = listaTemp!.FirstOrDefault(x => x.Id == data);
                else if (rol == "2" && int.TryParse(usuarioId, out int clienteId))
                    Alquiler = listaTemp!.FirstOrDefault(x => x.Id == data && x.Clientes == clienteId);
                else if (rol == "3" && int.TryParse(usuarioId, out int empleadoId))
                    Alquiler = listaTemp!.FirstOrDefault(x => x.Id == data && x.Empleados == empleadoId);
                else if (rol == "4" && int.TryParse(usuarioId, out int duenoId))
                {
                    var autosDueno = IAutosPresentacion!.Consultar()
                        .Where(a => a.Duenos.HasValue && a.Duenos.Value == duenoId)
                        .Select(a => a.Id)
                        .ToList();

                    Alquiler = listaTemp!.FirstOrDefault(x => x.Id == data && autosDueno.Contains(x.Autos));
                }

                if (Alquiler == null)
                    ViewData["Mensaje"] = "No tienes permiso para modificar este alquiler.";

                Lista = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Alquiler == null)
                    return;

                if (Alquiler.Clientes == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar un cliente.";
                    return;
                }

                if (Alquiler.Autos == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar un auto.";
                    return;
                }

                if (Alquiler.Empleados == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar un empleado.";
                    return;
                }

                var rol = HttpContext.Session.GetString("RolId");
                var usuarioId = HttpContext.Session.GetString("EntidadId");

                // Si es cliente o empleado, forzar su propio ID al guardar
                if (rol == "2" && int.TryParse(usuarioId, out int clienteId))
                    Alquiler.Clientes = clienteId;
                else if (rol == "3" && int.TryParse(usuarioId, out int empleadoId))
                    Alquiler.Empleados = empleadoId;

                if (Alquiler.Id == 0)
                    Alquiler = IAlquileresPresentacion!.Guardar(Alquiler!);
                else
                    Alquiler = IAlquileresPresentacion!.Modificar(Alquiler!);

                if (Alquiler.Id == 0)
                {
                    ViewData["Mensaje"] = "No fue posible guardar el alquiler.";
                    return;
                }

                ViewData["Mensaje"] = "Alquiler guardado correctamente.";

                CargarListaFiltrada();
                Alquiler = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                Exception errorReal = ex;

                while (errorReal.InnerException != null)
                    errorReal = errorReal.InnerException;

                ViewData["Mensaje"] = "Error al guardar el alquiler: " + errorReal.Message;

                CargarListaFiltrada();
            }
        }


        public void OnPostBtBorrar()
        {
            try
            {
                var rol = HttpContext.Session.GetString("RolId");
                var usuarioId = HttpContext.Session.GetString("EntidadId");

                var listaTemp = IAlquileresPresentacion!.Consultar();
                Alquileres? AlquilerPermitido = null;

                if (rol == "1")
                    AlquilerPermitido = listaTemp!.FirstOrDefault(x => x.Id == Alquiler!.Id);
                else if (rol == "2" && int.TryParse(usuarioId, out int clienteId))
                    AlquilerPermitido = listaTemp!.FirstOrDefault(x => x.Id == Alquiler!.Id && x.Clientes == clienteId);
                else if (rol == "3" && int.TryParse(usuarioId, out int empleadoId))
                    AlquilerPermitido = listaTemp!.FirstOrDefault(x => x.Id == Alquiler!.Id && x.Empleados == empleadoId);
                else if (rol == "4" && int.TryParse(usuarioId, out int duenoId))
                {
                    var autosDueno = IAutosPresentacion!.Consultar()
                        .Where(a => a.Duenos.HasValue && a.Duenos.Value == duenoId)
                        .Select(a => a.Id)
                        .ToList();

                    AlquilerPermitido = listaTemp!.FirstOrDefault(x => x.Id == Alquiler!.Id && autosDueno.Contains(x.Autos));
                }

                if (AlquilerPermitido == null)
                {
                    ViewData["Mensaje"] = "No tienes permiso para eliminar este alquiler.";
                    OnGet();
                    return;
                }

                Alquiler = IAlquileresPresentacion!.Eliminar(Alquiler!);

                CargarListaFiltrada();
                Alquiler = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
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