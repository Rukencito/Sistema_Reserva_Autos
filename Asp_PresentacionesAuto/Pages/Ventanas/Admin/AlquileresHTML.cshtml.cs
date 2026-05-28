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

        private void CargarListaFiltrada()
        {
            var rol = HttpContext.Session.GetString("RolId");
            var usuarioId = HttpContext.Session.GetString("EntidadId");

            ViewData["Debug"] = $"Rol='{rol}' | UsuarioId='{usuarioId}' | Presentacion={IAlquileresPresentacion != null}";

            Lista = IAlquileresPresentacion!.Consultar();

            if (rol == "2" && int.TryParse(usuarioId, out int clienteId))
                Lista = Lista!.Where(x => x.Clientes == clienteId).ToList();
            else if (rol == "3" && int.TryParse(usuarioId, out int empleadoId))
                Lista = Lista!.Where(x => x.Empleados == empleadoId).ToList();
        }

        public void OnGet()
        {
            try
            {
                CargarListaFiltrada();
                Alquiler = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Debug"] += $" | EXCEPCION: {ex.Message} | Inner: {ex.InnerException?.Message}";
            }
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

        public void OnPostBtGuardar()
        {
            try
            {
                if (Alquiler == null) return;

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

                if (Alquiler.Id == 0) return;

                CargarListaFiltrada();
                Alquiler = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
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

                Lista = IAlquileresPresentacion!.Consultar();

                //Admin
                if (rol == "1")
                {
                    Alquiler = Lista!.FirstOrDefault(x => x.Id == data);
                }
                
                //Cliente
                else if (rol == "2")
                {
                    Alquiler = Lista!.FirstOrDefault(x => 
                    x.Id == data && x.Clientes == int.Parse(usuarioId!));
                }

                //Empleado
                else if (rol == "3")
                {
                    Alquiler = Lista!.FirstOrDefault(x =>
                    x.Id == data && x.Empleados == int.Parse(usuarioId!));
                }

                if (Alquiler == null)
                {
                    ViewData["Mensaje"] = "No tienes permiso para modificar este alquiler.";
                }

                Lista = null;
                Borrando = false;
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
                var rol = HttpContext.Session.GetString("RolId");
                var usuarioId = HttpContext.Session.GetString("EntidadId");

                var Lista = IAlquileresPresentacion!.Consultar();

                Alquileres? AlquilerPermitido = null;

                //Admin
                if (rol == "1")
                {
                    AlquilerPermitido = Lista!.FirstOrDefault(x => x.Id == Alquiler!.Id);
                }

                //Cliente
                else if (rol == "2")
                {
                    AlquilerPermitido = Lista!.FirstOrDefault(x =>
                    x.Id == Alquiler!.Id && x.Clientes.ToString() == usuarioId!);
                }

                //Empleado
                else if (rol == "3")
                {
                    AlquilerPermitido = Lista!.FirstOrDefault(x =>
                    x.Id == Alquiler!.Id && x.Empleados.ToString() == usuarioId!);
                }

                if (Alquiler == null)
                {
                    ViewData["Mensaje"] = "No tienes permiso para modificar este alquiler.";
                }

                if (AlquilerPermitido == null)
                {
                    ViewData["Mensaje"] = "No tienes permiso para eliminar este alquiler.";
                    OnGet();
                    return;
                }

                Alquiler = IAlquileresPresentacion!.Eliminar(Alquiler!);

                OnGet();

                Lista = null;
                Borrando = false;
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