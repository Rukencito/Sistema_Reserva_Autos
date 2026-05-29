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

        public SucursalesHTMLModel()
        {
          ISucursalesPresentacion = new  SucursalesPresentacion();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
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
            Sucursal = new Sucursales();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
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
            try
            {
                if (Sucursal == null)
                    return;
                if (Sucursal.Id == 0)
                    Sucursal = ISucursalesPresentacion!.Guardar(Sucursal!);
                else
                    Sucursal = ISucursalesPresentacion!.Modificar(Sucursal!);
                if (Sucursal.Id == 0)
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
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}