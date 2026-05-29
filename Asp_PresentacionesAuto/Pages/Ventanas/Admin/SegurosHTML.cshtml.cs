using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class SegurosHTMLModel : PageModel
    {
        private ISegurosPresentacion? ISegurosPresentacion;
        private IAutosPresentacion? IAutosPresentacion;
        [BindProperty] public List<Seguros>? Lista { get; set; }
        [BindProperty] public List<Autos>? ListaAuto { get; set; }
        [BindProperty] public Seguros? Seguro { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public SegurosHTMLModel()
        {
            ISegurosPresentacion = new SegurosPresentacion();
            IAutosPresentacion = new AutosPresentacion();
        }

        private void CargarListaFiltrada()
        {
            var rol = HttpContext.Session.GetString("RolId");
            var entidadId = HttpContext.Session.GetString("EntidadId");

            Lista = ISegurosPresentacion!.Consultar();

            if (rol == "4" && int.TryParse(entidadId, out int duenoId))
            {
                var autosDueno = IAutosPresentacion!.Consultar()
                    .Where(a => a.Duenos.HasValue && a.Duenos.Value == duenoId)
                    .Select(a => a.Id)
                    .ToList();

                Lista = Lista!.Where(x => x.Autos.HasValue && autosDueno.Contains(x.Autos.Value)).ToList();
            }
        }

        public void OnGet()
        {
            try
            {
                CargarListaFiltrada();
                Seguro = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public List<Autos> ObtenerAutos()
        {
            var rol = HttpContext.Session.GetString("RolId");
            var entidadId = HttpContext.Session.GetString("EntidadId");

            var todos = IAutosPresentacion!.Consultar();

            // Dueño solo ve sus autos en el select
            if (rol == "4" && int.TryParse(entidadId, out int duenoId))
                return ListaAuto = todos.Where(a => a.Duenos.HasValue && a.Duenos.Value == duenoId).ToList();

            return ListaAuto = todos;
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                CargarListaFiltrada();
                Seguro = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Seguro = new Seguros();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                var rol = HttpContext.Session.GetString("RolId");
                var entidadId = HttpContext.Session.GetString("EntidadId");

                var listaTemp = ISegurosPresentacion!.Consultar();

                if (rol == "1")
                {
                    Seguro = listaTemp!.FirstOrDefault(x => x.Id == data);
                }
                else if (rol == "4" && int.TryParse(entidadId, out int duenoId))
                {
                    var autosDueno = IAutosPresentacion!.Consultar()
                        .Where(a => a.Duenos.HasValue && a.Duenos.Value == duenoId)
                        .Select(a => a.Id)
                        .ToList();

                    Seguro = listaTemp!.FirstOrDefault(x =>
                        x.Id == data && x.Autos.HasValue && autosDueno.Contains(x.Autos.Value));
                }

                if (Seguro == null)
                    ViewData["Mensaje"] = "No tienes permiso para modificar este seguro.";

                Lista = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Seguro == null) return;

                if (Seguro.Id == 0)
                    Seguro = ISegurosPresentacion!.Guardar(Seguro!);
                else
                    Seguro = ISegurosPresentacion!.Modificar(Seguro!);

                if (Seguro.Id == 0) return;

                CargarListaFiltrada();
                Seguro = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Seguro == null) return;

                var rol = HttpContext.Session.GetString("RolId");
                var entidadId = HttpContext.Session.GetString("EntidadId");

                var listaTemp = ISegurosPresentacion!.Consultar();
                Seguros? SeguroPermitido = null;

                if (rol == "1")
                {
                    SeguroPermitido = listaTemp!.FirstOrDefault(x => x.Id == Seguro!.Id);
                }
                else if (rol == "4" && int.TryParse(entidadId, out int duenoId))
                {
                    var autosDueno = IAutosPresentacion!.Consultar()
                        .Where(a => a.Duenos.HasValue && a.Duenos.Value == duenoId)
                        .Select(a => a.Id)
                        .ToList();

                    SeguroPermitido = listaTemp!.FirstOrDefault(x =>
                        x.Id == Seguro!.Id && x.Autos.HasValue && autosDueno.Contains(x.Autos.Value));
                }

                if (SeguroPermitido == null)
                {
                    ViewData["Mensaje"] = "No tienes permiso para eliminar este seguro.";
                    OnGet();
                    return;
                }

                Seguro = ISegurosPresentacion!.Eliminar(Seguro!);

                CargarListaFiltrada();
                Seguro = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                var rol = HttpContext.Session.GetString("RolId");
                var entidadId = HttpContext.Session.GetString("EntidadId");

                var listaTemp = ISegurosPresentacion!.Consultar();

                OnPostBtRefrescar();

                if (rol == "1")
                {
                    Seguro = listaTemp!.FirstOrDefault(x => x.Id == data);
                }
                else if (rol == "4" && int.TryParse(entidadId, out int duenoId))
                {
                    var autosDueno = IAutosPresentacion!.Consultar()
                        .Where(a => a.Duenos.HasValue && a.Duenos.Value == duenoId)
                        .Select(a => a.Id)
                        .ToList();

                    Seguro = listaTemp!.FirstOrDefault(x =>
                        x.Id == data && x.Autos.HasValue && autosDueno.Contains(x.Autos.Value));
                }

                if (Seguro == null)
                {
                    ViewData["Mensaje"] = "No tienes permiso para eliminar este seguro.";
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
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}