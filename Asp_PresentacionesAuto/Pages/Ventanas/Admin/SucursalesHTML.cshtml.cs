using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class SucursalesHTMLModel : PageModel
    {
        private ISucursalesPresentacion? ISucursalesPresentacion;
        [BindProperty] public List<Sucursales>? Lista { get; set; }
        [BindProperty] public Sucursales? Sucursal { get; set; }
        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public bool TieneError { get; set; }

        public SucursalesHTMLModel()
        {
        }
        private void IniciarSucursales()
        {
            var correo = HttpContext.Session.GetString("Usuario") ?? "Sistema";
            ISucursalesPresentacion = new SucursalesPresentacion(correo);
        }

        public void OnGet()
        {
            IniciarSucursales();
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            IniciarSucursales();
            try
            {
                if (ISucursalesPresentacion == null)
                    return;
                Lista = ISucursalesPresentacion.Consultar();
                Sucursal = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            IniciarSucursales();
            Sucursal = new Sucursales();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            IniciarSucursales();
            try
            {
                OnPostBtRefrescar();
                Sucursal = Lista!.FirstOrDefault(x => x.Id == data);
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
            IniciarSucursales();
            try
            {
                if (Sucursal == null)
                    return;

                if (Sucursal.Id == 0)
                    Sucursal = ISucursalesPresentacion!.Guardar(Sucursal!);
                else
                    Sucursal = ISucursalesPresentacion!.Modificar(Sucursal!);

                if (Sucursal.Id == 0)
                {
                    ViewData["Mensaje"] = "No fue posible guardar la sucursal.";
                    return;
                }

                ViewData["Mensaje"] = "Sucursal guardada correctamente.";

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
            IniciarSucursales();
            try
            {
                if (Sucursal == null)
                    return;
                Sucursal = ISucursalesPresentacion!.Eliminar(Sucursal!);
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrarVal(int data)
        {
            IniciarSucursales();
            try
            {
                OnPostBtRefrescar();
                Sucursal = Lista!.FirstOrDefault(x => x.Id == data);
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
            IniciarSucursales();
            if (TieneError)
            {
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