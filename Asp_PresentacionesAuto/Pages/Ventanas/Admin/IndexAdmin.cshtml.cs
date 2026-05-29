using Asp_PresentacionesAuto.Helpers;
using ClosedXML.Excel;
using Lib_Presentacion_Autos.Implementaciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class IndexAdminModel : PageModel
    {
        public void OnGet() { }

        public IActionResult OnPostDescargarReporte()
        {
            try
            {
                using var wb = new XLWorkbook();

                var usuarios = new UsuariosPresentacion().Consultar();
                var roles = new RolesPresentacion().Consultar();
                var permisos = new PermisosPresentacion().Consultar();
                var clientes = new ClientesPresentacion().Consultar();
                var empleados = new EmpleadosPresentacion().Consultar();
                var duenos = new DuenosPresentacion().Consultar();
                var autos = new AutosPresentacion().Consultar();
                var parqueaderos = new ParqueaderosPresentacion().Consultar();
                var sucursales = new SucursalesPresentacion().Consultar();
                var talleres = new TalleresPresentacion().Consultar();
                var inventarios = new InventariosPresentacion().Consultar();
                var garantias = new GarantiasPresentacion().Consultar();
                var seguros = new SegurosPresentacion().Consultar();
                var mantenimientos = new MantenimientosPresentacion().Consultar();
                var ventas = new VentasPresentacion().Consultar();
                var alquileres = new AlquileresPresentacion().Consultar();
                var reservas = new ReservasPresentacion().Consultar();
                var devoluciones = new DevolucionesPresentacion().Consultar();
                var contratos = new ContratosPresentacion().Consultar();
                var promociones = new PromocionesPresentacion().Consultar();
                var resenas = new ResenasPresentacion().Consultar();
                var facturas = new FacturasPresentacion().Consultar();
                var detalles = new DetallesFacturaPresentacion().Consultar();
                var pagos = new PagosPresentacion().Consultar();

                ReportHelper.AgregarHoja(wb, usuarios, "Usuarios");
                ReportHelper.AgregarHoja(wb, roles, "Roles");
                ReportHelper.AgregarHoja(wb, permisos, "Permisos");
                ReportHelper.AgregarHoja(wb, clientes, "Clientes");
                ReportHelper.AgregarHoja(wb, empleados, "Empleados");
                ReportHelper.AgregarHoja(wb, duenos, "Dueños");
                ReportHelper.AgregarHoja(wb, autos, "Autos");
                ReportHelper.AgregarHoja(wb, parqueaderos, "Parqueaderos");
                ReportHelper.AgregarHoja(wb, sucursales, "Sucursales");
                ReportHelper.AgregarHoja(wb, talleres, "Talleres");
                ReportHelper.AgregarHoja(wb, inventarios, "Inventarios");
                ReportHelper.AgregarHoja(wb, garantias, "Garantías");
                ReportHelper.AgregarHoja(wb, seguros, "Seguros");
                ReportHelper.AgregarHoja(wb, mantenimientos, "Mantenimientos");
                ReportHelper.AgregarHoja(wb, ventas, "Ventas");
                ReportHelper.AgregarHoja(wb, alquileres, "Alquileres");
                ReportHelper.AgregarHoja(wb, reservas, "Reservas");
                ReportHelper.AgregarHoja(wb, devoluciones, "Devoluciones");
                ReportHelper.AgregarHoja(wb, contratos, "Contratos");
                ReportHelper.AgregarHoja(wb, promociones, "Promociones");
                ReportHelper.AgregarHoja(wb, resenas, "Reseñas");
                ReportHelper.AgregarHoja(wb, facturas, "Facturas");
                ReportHelper.AgregarHoja(wb, detalles, "Detalles Factura");
                ReportHelper.AgregarHoja(wb, pagos, "Pagos");

                ReportHelper.AgregarResumen(wb, "Reporte General — Administrador");

                using var ms = new MemoryStream();
                wb.SaveAs(ms);

                return File(
                    ms.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"Reporte_Admin_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
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