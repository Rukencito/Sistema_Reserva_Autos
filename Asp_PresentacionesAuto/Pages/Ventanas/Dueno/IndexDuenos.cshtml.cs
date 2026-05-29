using Asp_PresentacionesAuto.Helpers;
using ClosedXML.Excel;
using Lib_Presentacion_Autos.Implementaciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Dueno
{
    public class IndexDuenosModel : PageModel
    {
        public void OnGet() { }

        public IActionResult OnPostDescargarReporte()
        {
            try
            {
                var duenoIdStr = HttpContext.Session.GetString("EntidadId");
                if (!int.TryParse(duenoIdStr, out int duenoId))
                    return RedirectToPage("/Login");

                var autos = new AutosPresentacion().Consultar()
                    .Where(x => x.Duenos == duenoId).ToList();

                var autoIds = autos.Select(a => a.Id).ToHashSet();

                var ventas = new VentasPresentacion().Consultar()
                    .Where(x => x.Autos.HasValue && autoIds.Contains(x.Autos.Value)).ToList();

                var mantenimientos = new MantenimientosPresentacion().Consultar()
                    .Where(x => x.Autos.HasValue && autoIds.Contains(x.Autos.Value)).ToList();

                var garantias = new GarantiasPresentacion().Consultar()
                    .Where(x => autoIds.Contains(x.Autos)).ToList();

                var seguros = new SegurosPresentacion().Consultar()
                    .Where(x => x.Autos.HasValue && autoIds.Contains(x.Autos.Value)).ToList();

                using var wb = new XLWorkbook();

                ReportHelper.AgregarHoja(wb, autos, "Autos");
                ReportHelper.AgregarHoja(wb, ventas, "Ventas");
                ReportHelper.AgregarHoja(wb, mantenimientos, "Mantenimientos");
                ReportHelper.AgregarHoja(wb, garantias, "Garantías");
                ReportHelper.AgregarHoja(wb, seguros, "Seguros");

                ReportHelper.AgregarResumen(wb, "Reporte — Dueño");

                using var ms = new MemoryStream();
                wb.SaveAs(ms);

                return File(
                    ms.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"Reporte_Dueno_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
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