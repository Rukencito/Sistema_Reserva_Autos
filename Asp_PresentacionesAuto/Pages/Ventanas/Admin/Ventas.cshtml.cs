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
          IVentasPresentacion = new  VentasPresentacion();
            IClientesPresentacion = new ClientesPresentacion();
            IEmpleadosPresentacion = new EmpleadosPresentacion();
            IAutosPresentacion = new AutosPresentacion();
        }

        public List<Clientes> ObtenerClientes()
        {
            return ListaCliente = IClientesPresentacion!.Consultar();
        }

        public List<Empleados> ObtenerEmpleados()
        {
            return ListaEmpleado = IEmpleadosPresentacion!.Consultar();
        }

        public List<Autos> ObtenerAutos()
        {
            return ListaAuto = IAutosPresentacion!.Consultar();
        }



        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (IVentasPresentacion == null)
                    return;

                Lista = IVentasPresentacion.Consultar();

                Venta = null;

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
            Venta = new Ventas();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Venta = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Venta == null)
                    return;
                if (Venta.Id == 0)
                    Venta = IVentasPresentacion!.Guardar(Venta!);
                else
                    Venta = IVentasPresentacion!.Modificar(Venta!);
                if (Venta.Id == 0)
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
                if (Venta == null)
                    return;
                Venta = IVentasPresentacion!.Eliminar(Venta!);
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
                Venta = Lista!.FirstOrDefault(x => x.Id == data);
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