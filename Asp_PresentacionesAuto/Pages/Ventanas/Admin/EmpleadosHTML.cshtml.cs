using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class EmpleadosHTMLModel : PageModel
    {
        private IEmpleadosPresentacion? IEmpleadosPresentacion;
        private ISucursalesPresentacion? ISucursalesPresentacion;
        [BindProperty] public List<Empleados>? Lista { get; set; }
        [BindProperty] public List<Sucursales>? ListaSucursal { get; set; }
        [BindProperty] public Empleados? Empleado { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public EmpleadosHTMLModel()
        {
        }

        private void IniciarEmpleados()
        {
            var correo = HttpContext.Session.GetString("Usuario") ?? "Sistema";
            IEmpleadosPresentacion = new EmpleadosPresentacion(correo);
            ISucursalesPresentacion = new SucursalesPresentacion(correo);
        }

        public List<Sucursales> ObtenerSucursales()
        {
            return ListaSucursal = ISucursalesPresentacion!.Consultar();
        }

        public void OnGet()
        {
            IniciarEmpleados();
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            IniciarEmpleados();
            try
            {
                if (IEmpleadosPresentacion == null)
                    return;
                Lista = IEmpleadosPresentacion.Consultar();
                Empleado = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            IniciarEmpleados();
            Empleado = new Empleados();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            IniciarEmpleados();
            try
            {
                OnPostBtRefrescar();
                Empleado = Lista!.FirstOrDefault(x => x.Id == data);
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
            IniciarEmpleados();
            try
            {
                if (Empleado == null)
                    return;

                if ((Empleado.Sucursales ?? 0) == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar una sucursal.";
                    return;
                }

                if (Empleado.Id == 0)
                    Empleado = IEmpleadosPresentacion!.Guardar(Empleado!);
                else
                    Empleado = IEmpleadosPresentacion!.Modificar(Empleado!);

                if (Empleado.Id == 0)
                {
                    ViewData["Mensaje"] = "No fue posible guardar el empleado.";
                    return;
                }

                ViewData["Mensaje"] = "Empleado guardado correctamente.";

                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                Exception errorReal = ex;

                while (errorReal.InnerException != null)
                    errorReal = errorReal.InnerException;

                ViewData["Mensaje"] = errorReal.Message;

                OnPostBtRefrescar();
            }
        }

        public void OnPostBtBorrar()
        {
            IniciarEmpleados();
            try
            {
                if (Empleado == null)
                    return;
                Empleado = IEmpleadosPresentacion!.Eliminar(Empleado!);
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrarVal(int data)
        {
            IniciarEmpleados();
            try
            {
                OnPostBtRefrescar();
                Empleado = Lista!.FirstOrDefault(x => x.Id == data);
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
            IniciarEmpleados();
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}