using Asp_PresentacionesAuto.Helpers;
using ClosedXML.Excel;
using Lib_Presentacion_Autos.Implementaciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Cliente
{
    public class IndexClientesModel : PageModel
    {
        public void OnGet() { }

        public IActionResult OnPostDescargarReporte()
        {
            try
            {
                var clienteIdStr = HttpContext.Session.GetString("EntidadId");
                if (!int.TryParse(clienteIdStr, out int clienteId))
                    return RedirectToPage("/Login");

                var alquileres = new AlquileresPresentacion().Consultar()
                    .Where(x => x.Clientes == clienteId).ToList();

                var reservas = new ReservasPresentacion().Consultar()
                    .Where(x => x.Clientes == clienteId).ToList();

                var contratos = new ContratosPresentacion().Consultar()
                    .Where(x => alquileres.Select(a => a.Id).Contains(x.Alquileres)).ToList();

                var facturas = new FacturasPresentacion().Consultar()
                    .Where(x => x.Clientes == clienteId).ToList();

                var facturaIds = facturas.Select(f => f.Id).ToHashSet();

                var detalles = new DetallesFacturaPresentacion().Consultar()
                    .Where(x => facturaIds.Contains(x.Facturas)).ToList();

                var pagos = new PagosPresentacion().Consultar()
                    .Where(x => x.Facturas.HasValue && facturaIds.Contains(x.Facturas.Value)).ToList();

                var ventas = new VentasPresentacion().Consultar()
                    .Where(x => x.Clientes == clienteId).ToList();

                var resenas = new ResenasPresentacion().Consultar()
                    .Where(x => x.Clientes == clienteId).ToList();

                using var wb = new XLWorkbook();

                ReportHelper.AgregarHoja(wb, alquileres, "Alquileres");
                ReportHelper.AgregarHoja(wb, reservas, "Reservas");
                ReportHelper.AgregarHoja(wb, contratos, "Contratos");
                ReportHelper.AgregarHoja(wb, facturas, "Facturas");
                ReportHelper.AgregarHoja(wb, detalles, "Detalles Factura");
                ReportHelper.AgregarHoja(wb, pagos, "Pagos");
                ReportHelper.AgregarHoja(wb, ventas, "Ventas");
                ReportHelper.AgregarHoja(wb, resenas, "Reseñas");

                ReportHelper.AgregarResumen(wb, "Reporte — Cliente");

                using var ms = new MemoryStream();
                wb.SaveAs(ms);

                return File(
                    ms.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"Reporte_Cliente_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
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