using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class ContratosHTMLModel : PageModel
    {
        private IContratosPresentacion? IContratosPresentacion;
        private IAlquileresPresentacion? IAlquileresPresentacion;
        [BindProperty] public List<Contratos>? Lista { get; set; }
        [BindProperty] public List<Alquileres>? ListaAlquiler { get; set; }
        [BindProperty] public Contratos? Contrato { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public ContratosHTMLModel()
        {
        }

        private void IniciarContratos()
        {
            var correo = HttpContext.Session.GetString("Usuario") ?? "Sistema";
            IContratosPresentacion = new ContratosPresentacion(correo);
            IAlquileresPresentacion = new AlquileresPresentacion(correo);

        }


        public List<Alquileres> ObtenerAlquileres()
        {
            return ListaAlquiler = IAlquileresPresentacion!.Consultar();
        }

        private void CargarListaFiltrada()
        {
            var rol = HttpContext.Session.GetString("RolId");
            var entidadId = HttpContext.Session.GetString("EntidadId");

            Lista = IContratosPresentacion!.Consultar();

            if (rol == "2" && int.TryParse(entidadId, out int clienteId))
            {
                var alquileresCliente = IAlquileresPresentacion!.Consultar()
                    .Where(a => a.Clientes == clienteId)
                    .Select(a => a.Id)
                    .ToList();

                Lista = Lista!.Where(x => alquileresCliente.Contains(x.Alquileres)).ToList();
            }
            else if (rol == "3" && int.TryParse(entidadId, out int empleadoId))
            {
                var alquileresEmpleado = IAlquileresPresentacion!.Consultar()
                    .Where(a => a.Empleados == empleadoId)
                    .Select(a => a.Id)
                    .ToList();

                Lista = Lista!.Where(x => alquileresEmpleado.Contains(x.Alquileres)).ToList();
            }
        }


        public void OnGet()
        {
            IniciarContratos();
            try
            {
                CargarListaFiltrada();
                Contrato = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }


        public void OnPostBtRefrescar()
        {
            IniciarContratos();
            try
            {
                CargarListaFiltrada();
                Contrato = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            IniciarContratos();
            Contrato = new Contratos();
            Lista = null;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            IniciarContratos();
            try
            {
                var rol = HttpContext.Session.GetString("RolId");
                var entidadId = HttpContext.Session.GetString("EntidadId");

                var listaTemp = IContratosPresentacion!.Consultar();

                // Admin 
                if (rol == "1")
                {
                    Contrato = listaTemp!.FirstOrDefault(x => x.Id == data);
                }
                // Cliente 
                else if (rol == "2" && int.TryParse(entidadId, out int clienteId))
                {
                    var alquileresCliente = IAlquileresPresentacion!.Consultar()
                        .Where(a => a.Clientes == clienteId)
                        .Select(a => a.Id)
                        .ToList();

                    Contrato = listaTemp!.FirstOrDefault(x =>
                        x.Id == data && alquileresCliente.Contains(x.Alquileres));
                }
                // Empleado 
                else if (rol == "3" && int.TryParse(entidadId, out int empleadoId))
                {
                    var alquileresEmpleado = IAlquileresPresentacion!.Consultar()
                        .Where(a => a.Empleados == empleadoId)
                        .Select(a => a.Id)
                        .ToList();

                    Contrato = listaTemp!.FirstOrDefault(x =>
                        x.Id == data && alquileresEmpleado.Contains(x.Alquileres));
                }

                if (Contrato == null)
                    ViewData["Mensaje"] = "No tienes permiso para modificar este contrato.";

                Lista = null;
                Borrando = false;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtGuardar()
        {
            IniciarContratos();
            try
            {
                if (Contrato == null)
                    return;

                if (Contrato.Alquileres == 0)
                {
                    ViewData["Mensaje"] = "Debe seleccionar un alquiler.";
                    return;
                }

                if (Contrato.Id == 0)
                    Contrato = IContratosPresentacion!.Guardar(Contrato!);
                else
                    Contrato = IContratosPresentacion!.Modificar(Contrato!);

                if (Contrato.Id == 0)
                {
                    ViewData["Mensaje"] = "No fue posible guardar el contrato.";
                    return;
                }

                ViewData["Mensaje"] = "Contrato guardado correctamente.";

                CargarListaFiltrada();
                Contrato = null;
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
            IniciarContratos();
            try
            {
                if (Contrato == null) return;

                var rol = HttpContext.Session.GetString("RolId");
                var entidadId = HttpContext.Session.GetString("EntidadId");

                var listaTemp = IContratosPresentacion!.Consultar();
                Contratos? ContratoPermitido = null;

                // Admin 
                if (rol == "1")
                {
                    ContratoPermitido = listaTemp!.FirstOrDefault(x => x.Id == Contrato!.Id);
                }
                // Cliente 
                else if (rol == "2" && int.TryParse(entidadId, out int clienteId))
                {
                    var alquileresCliente = IAlquileresPresentacion!.Consultar()
                        .Where(a => a.Clientes == clienteId)
                        .Select(a => a.Id)
                        .ToList();

                    ContratoPermitido = listaTemp!.FirstOrDefault(x =>
                        x.Id == Contrato!.Id && alquileresCliente.Contains(x.Alquileres));
                }
                // Empleado 
                else if (rol == "3" && int.TryParse(entidadId, out int empleadoId))
                {
                    var alquileresEmpleado = IAlquileresPresentacion!.Consultar()
                        .Where(a => a.Empleados == empleadoId)
                        .Select(a => a.Id)
                        .ToList();

                    ContratoPermitido = listaTemp!.FirstOrDefault(x =>
                        x.Id == Contrato!.Id && alquileresEmpleado.Contains(x.Alquileres));
                }

                if (ContratoPermitido == null)
                {
                    ViewData["Mensaje"] = "No tienes permiso para eliminar este contrato.";
                    OnGet();
                    return;
                }

                Contrato = IContratosPresentacion!.Eliminar(Contrato!);

                CargarListaFiltrada();
                Contrato = null;
                Borrando = false;
                ModelState.Clear();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrarVal(int data)
        {
            IniciarContratos();
            try
            {
                OnPostBtRefrescar();
                Contrato = Lista!.FirstOrDefault(x => x.Id == data);
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
            IniciarContratos();
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}