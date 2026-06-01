using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Lib_Presentacion_Autos.Implementaciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class GarantiasHTMLModel : PageModel
    {
        private IGarantiasPresentacion? IGarantiasPresentacion;
        private IAutosPresentacion? IAutosPresentacion;
        [BindProperty] public List<Garantias>? Lista { get; set; }
        [BindProperty] public List<Autos>? ListaAuto { get; set; }
        [BindProperty] public Garantias? Garantia { get; set; }
        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public bool TieneError { get; set; }

        public GarantiasHTMLModel()
        {
        }
        private void IniciarGarantias()
        {
            var correo = HttpContext.Session.GetString("Usuario") ?? "Sistema";
            IGarantiasPresentacion = new GarantiasPresentacion(correo);
            IAutosPresentacion = new AutosPresentacion(correo);
        }
        public List<Autos> ObtenerAutos()
        {
            return ListaAuto = IAutosPresentacion!.Consultar();
        }

        private void CargarListaFiltrada()
        {
            var rol = HttpContext.Session.GetString("RolId");
            var entidadId = HttpContext.Session.GetString("EntidadId");

            Lista = IGarantiasPresentacion!.Consultar();

            if (rol == "4" && int.TryParse(entidadId, out int duenoId))
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
            IniciarGarantias();
            try
            {
                CargarListaFiltrada();
                Garantia = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtRefrescar()
        {
            IniciarGarantias();
            try
            {
                CargarListaFiltrada();
                Garantia = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            IniciarGarantias();
            Garantia = new Garantias();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            IniciarGarantias();
            try
            {
                var rol = HttpContext.Session.GetString("RolId");
                var entidadId = HttpContext.Session.GetString("EntidadId");

                var listaTemp = IGarantiasPresentacion!.Consultar();

                if (rol == "1")
                {
                    Garantia = listaTemp!.FirstOrDefault(x => x.Id == data);
                }
                else if (rol == "4" && int.TryParse(entidadId, out int duenoId))
                {
                    var autosDueno = IAutosPresentacion!.Consultar()
                        .Where(a => a.Duenos.HasValue && a.Duenos.Value == duenoId)
                        .Select(a => a.Id)
                        .ToList();

                    Garantia = listaTemp!.FirstOrDefault(x =>
                        x.Id == data && autosDueno.Contains(x.Autos));
                }

                if (Garantia == null)
                    ViewData["Mensaje"] = "No tienes permiso para modificar esta garantía.";

                Lista = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtGuardar()
        {
            IniciarGarantias();
            try
            {
                if (Garantia == null)
                    return;

                if (Garantia.Autos == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar un auto.";
                    return;
                }

                if (Garantia.Id == 0)
                    Garantia = IGarantiasPresentacion!.Guardar(Garantia!);
                else
                    Garantia = IGarantiasPresentacion!.Modificar(Garantia!);

                if (Garantia.Id == 0)
                {
                    ViewData["Mensaje"] = "No fue posible guardar la garantía.";
                    return;
                }

                ViewData["Mensaje"] = "Garantía guardada correctamente.";

                CargarListaFiltrada();
                Garantia = null;
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
            IniciarGarantias();
            try
            {
                if (Garantia == null) return;

                var rol = HttpContext.Session.GetString("RolId");
                var entidadId = HttpContext.Session.GetString("EntidadId");

                var listaTemp = IGarantiasPresentacion!.Consultar();
                Garantias? GarantiaPermitida = null;

                if (rol == "1")
                {
                    GarantiaPermitida = listaTemp!.FirstOrDefault(x => x.Id == Garantia!.Id);
                }
                else if (rol == "4" && int.TryParse(entidadId, out int duenoId))
                {
                    var autosDueno = IAutosPresentacion!.Consultar()
                        .Where(a => a.Duenos.HasValue && a.Duenos.Value == duenoId)
                        .Select(a => a.Id)
                        .ToList();

                    GarantiaPermitida = listaTemp!.FirstOrDefault(x =>
                        x.Id == Garantia!.Id && autosDueno.Contains(x.Autos));
                }

                if (GarantiaPermitida == null)
                {
                    ViewData["Mensaje"] = "No tienes permiso para eliminar esta garantía.";
                    OnGet();
                    return;
                }

                Garantia = IGarantiasPresentacion!.Eliminar(Garantia!);

                CargarListaFiltrada();
                Garantia = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrarVal(int data)
        {
            IniciarGarantias();
            try
            {
                var rol = HttpContext.Session.GetString("RolId");
                var entidadId = HttpContext.Session.GetString("EntidadId");

                var listaTemp = IGarantiasPresentacion!.Consultar();

                OnPostBtRefrescar();

                if (rol == "1")
                {
                    Garantia = listaTemp!.FirstOrDefault(x => x.Id == data);
                }
                else if (rol == "4" && int.TryParse(entidadId, out int duenoId))
                {
                    var autosDueno = IAutosPresentacion!.Consultar()
                        .Where(a => a.Duenos.HasValue && a.Duenos.Value == duenoId)
                        .Select(a => a.Id)
                        .ToList();

                    Garantia = listaTemp!.FirstOrDefault(x =>
                        x.Id == data && autosDueno.Contains(x.Autos));
                }

                if (Garantia == null)
                {
                    ViewData["Mensaje"] = "No tienes permiso para eliminar esta garantía.";
                    CargarListaFiltrada();
                    return;
                }

                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtCerrar()
        {
            IniciarGarantias();
            if (TieneError)
            {
                CargarListaFiltrada();
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