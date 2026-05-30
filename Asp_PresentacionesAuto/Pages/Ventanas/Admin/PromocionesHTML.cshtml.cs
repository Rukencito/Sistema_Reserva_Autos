using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class PromocionesHTMLModel : PageModel
    {
        private IPromocionesPresentacion? IPromocionesPresentacion;
        private IVentasPresentacion? IVentasPresentacion;
        [BindProperty] public List<Promociones>? Lista { get; set; }
        [BindProperty] public List<Ventas>? ListaVenta { get; set; }
        [BindProperty] public Promociones? Promocion { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public PromocionesHTMLModel()
        {
          IPromocionesPresentacion = new  PromocionesPresentacion();
            IVentasPresentacion = new VentasPresentacion();
        }
        private void IniciarPromociones()
        {
            var correo = HttpContext.Session.GetString("Usuario") ?? "Sistema";
            IPromocionesPresentacion = new PromocionesPresentacion(correo);
        }

        public void OnGet()
        {
            IniciarPromociones();
            OnPostBtRefrescar();
        }

        public List<Ventas> ObtenerVentas()
        {
            return ListaVenta = IVentasPresentacion!.Consultar();
        }

        public void OnPostBtRefrescar()
        {
            IniciarPromociones();
            try
            {
                if (IPromocionesPresentacion == null)
                    return;
                Lista = IPromocionesPresentacion.Consultar();
                Promocion = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            IniciarPromociones();
            Promocion = new Promociones();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            IniciarPromociones();
            try
            {
                OnPostBtRefrescar();
                Promocion = Lista!.FirstOrDefault(x => x.Id == data);
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
            IniciarPromociones();
            try
            {
                if (Promocion == null)
                    return;

                if ((Promocion.Ventas ?? 0) == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar una venta.";
                    return;
                }

                if (Promocion.Id == 0)
                    Promocion = IPromocionesPresentacion!.Guardar(Promocion!);
                else
                    Promocion = IPromocionesPresentacion!.Modificar(Promocion!);

                if (Promocion.Id == 0)
                {
                    ViewData["Mensaje"] = "No fue posible guardar la promoción.";
                    return;
                }

                ViewData["Mensaje"] = "Promoción guardada correctamente.";

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
            IniciarPromociones();
            try
            {
                if (Promocion == null)
                    return;
                Promocion = IPromocionesPresentacion!.Eliminar(Promocion!);
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrarVal(int data)
        {
            IniciarPromociones();
            try
            {
                OnPostBtRefrescar();
                Promocion = Lista!.FirstOrDefault(x => x.Id == data);
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
            IniciarPromociones();
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}