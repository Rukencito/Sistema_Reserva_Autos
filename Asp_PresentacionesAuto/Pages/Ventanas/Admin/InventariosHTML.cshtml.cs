using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class InventariosHTMLModel : PageModel
    {
        private IInventariosPresentacion? IInventariosPresentacion;
        [BindProperty] public List<Inventarios>? Lista { get; set; }
        [BindProperty] public Inventarios? Inventario { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public InventariosHTMLModel()
        {
            IInventariosPresentacion = new InventariosPresentacion();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (IInventariosPresentacion == null)
                    return;
                Lista = IInventariosPresentacion.Consultar();
                Inventario = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Inventario = new Inventarios();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Inventario = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Inventario == null)
                    return;
                if (Inventario.Id == 0)
                    Inventario = IInventariosPresentacion!.Guardar(Inventario!);
                else
                    Inventario = IInventariosPresentacion!.Modificar(Inventario!);
                if (Inventario.Id == 0)
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
                if (Inventario == null)
                    return;
                Inventario = IInventariosPresentacion!.Eliminar(Inventario!);
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
                Inventario = Lista!.FirstOrDefault(x => x.Id == data);
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