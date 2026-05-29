using Asp_PresentacionesAuto.Helpers;
using ClosedXML.Excel;
using Lib_Presentacion_Autos.Implementaciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Empleado
{
    public class IndexEmpleadosModel : PageModel
    {
        public void OnGet() { }

        public IActionResult OnPostDescargarReporte()
        {
            try
            {
                var empleadoIdStr = HttpContext.Session.GetString("EntidadId");
                if (!int.TryParse(empleadoIdStr, out int empleadoId))
                    return RedirectToPage("/Login");

                var alquileres = new AlquileresPresentacion().Consultar()
                    .Where(x => x.Empleados == empleadoId).ToList();

                var alquilerIds = alquileres.Select(a => a.Id).ToHashSet();

                var reservas = new ReservasPresentacion().Consultar().ToList();   // todas

                var contratos = new ContratosPresentacion().Consultar()
                    .Where(x => alquilerIds.Contains(x.Alquileres)).ToList();

                var devoluciones = new DevolucionesPresentacion().Consultar()
                    .Where(x => alquilerIds.Contains(x.Alquileres)).ToList();

                var clientes = new ClientesPresentacion().Consultar();
                var autos = new AutosPresentacion().Consultar();
                var mantenimientos = new MantenimientosPresentacion().Consultar();

                using var wb = new XLWorkbook();

                ReportHelper.AgregarHoja(wb, alquileres, "Alquileres");
                ReportHelper.AgregarHoja(wb, reservas, "Reservas");
                ReportHelper.AgregarHoja(wb, contratos, "Contratos");
                ReportHelper.AgregarHoja(wb, clientes, "Clientes");
                ReportHelper.AgregarHoja(wb, autos, "Autos");
                ReportHelper.AgregarHoja(wb, devoluciones, "Devoluciones");
                ReportHelper.AgregarHoja(wb, mantenimientos, "Mantenimientos");

                ReportHelper.AgregarResumen(wb, "Reporte — Empleado");

                using var ms = new MemoryStream();
                wb.SaveAs(ms);

                return File(
                    ms.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"Reporte_Empleado_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
                );
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
                return Page();
            }
        }
    }
}