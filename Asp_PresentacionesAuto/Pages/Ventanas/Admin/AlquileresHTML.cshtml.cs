using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public void OnGet()
        {
            OnPostBtRefrescar();
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

        public void OnPostBtRefrescar()
        {
            try
            {
                if (IAlquileresPresentacion == null)
                    return;

                Lista = IAlquileresPresentacion.Consultar();

                Alquiler = null;

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
            Alquiler = new Alquileres();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Alquiler = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Alquiler == null)
                    return;
                if (Alquiler.Id == 0)
                    Alquiler = IAlquileresPresentacion!.Guardar(Alquiler!);
                else
                    Alquiler = IAlquileresPresentacion!.Modificar(Alquiler!);
                if (Alquiler.Id == 0)
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
                if (Alquiler == null)
                    return;
                Alquiler = IAlquileresPresentacion!.Eliminar(Alquiler!);
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