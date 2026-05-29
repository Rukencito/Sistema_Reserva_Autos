using ClosedXML.Excel;
using System.Reflection;

namespace Asp_PresentacionesAuto.Helpers
{
    public static class ReportHelper
    {
        public static void AgregarHoja<T>(XLWorkbook wb, IEnumerable<T> datos, string nombreHoja)
        {
            var ws = wb.Worksheets.Add(nombreHoja);

            var props = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(EsPropiedadSimple)
                .ToArray();

            for (int c = 0; c < props.Length; c++)
            {
                var cell = ws.Cell(1, c + 1);
                cell.Value = props[c].Name;
                cell.Style.Font.Bold = true;
                cell.Style.Font.FontName = "Arial";
                cell.Style.Font.FontColor = XLColor.White;
                cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#1a3a5c");
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }

            int fila = 2;
            bool impar = true;
            foreach (var item in datos)
            {
                var bg = impar ? XLColor.FromHtml("#edf2f7") : XLColor.White;
                for (int c = 0; c < props.Length; c++)
                {
                    var valor = props[c].GetValue(item);
                    var cell  = ws.Cell(fila, c + 1);
                    cell.Style.Font.FontName = "Arial";
                    cell.Style.Font.FontSize = 9;
                    cell.Style.Fill.BackgroundColor = bg;

                    switch (valor)
                    {
                        case DateTime dt:
                            cell.Value = dt.ToString("yyyy-MM-dd HH:mm");
                            break;
                        case bool b:
                            cell.Value = b ? "Sí" : "No";
                            break;
                        case decimal d:
                            cell.Value = d;
                            cell.Style.NumberFormat.Format = "#,##0.00";
                            break;
                        case null:
                            cell.Value = "";
                            break;
                        default:
                            cell.Value = valor.ToString();
                            break;
                    }
                }
                fila++;
                impar = !impar;
            }

            ws.Range(1, 1, 1, props.Length)
              .Style.Border.BottomBorder = XLBorderStyleValues.Medium;

            ws.Columns().AdjustToContents();
        }

        public static void AgregarResumen(XLWorkbook wb, string tituloReporte)
        {
            var ws = wb.Worksheets.Add("Resumen", 1); // insertar al inicio

            ws.Cell("B2").Value = tituloReporte;
            ws.Cell("B2").Style.Font.Bold = true;
            ws.Cell("B2").Style.Font.FontSize = 16;
            ws.Cell("B2").Style.Font.FontName = "Arial";
            ws.Cell("B2").Style.Font.FontColor = XLColor.FromHtml("#1a3a5c");

            ws.Cell("B3").Value = $"Generado el: {DateTime.Now:yyyy-MM-dd HH:mm}";
            ws.Cell("B3").Style.Font.FontSize = 9;
            ws.Cell("B3").Style.Font.FontColor = XLColor.Gray;

            int fila = 5;
            ws.Cell(fila, 2).Value = "Hoja";
            ws.Cell(fila, 3).Value = "Registros";
            foreach (var col in new[] { 2, 3 })
            {
                ws.Cell(fila, col).Style.Font.Bold = true;
                ws.Cell(fila, col).Style.Fill.BackgroundColor = XLColor.FromHtml("#1a3a5c");
                ws.Cell(fila, col).Style.Font.FontColor = XLColor.White;
                ws.Cell(fila, col).Style.Font.FontName = "Arial";
            }

            fila++;
            bool impar = true;
            foreach (var hoja in wb.Worksheets.Where(h => h.Name != "Resumen"))
            {
                var bg = impar ? XLColor.FromHtml("#edf2f7") : XLColor.White;
                ws.Cell(fila, 2).Value = hoja.Name;
                ws.Cell(fila, 3).Value = hoja.LastRowUsed()?.RowNumber() - 1 ?? 0; // -1 por encabezado
                ws.Cell(fila, 2).Style.Fill.BackgroundColor = bg;
                ws.Cell(fila, 3).Style.Fill.BackgroundColor = bg;
                ws.Cell(fila, 2).Style.Font.FontName = "Arial";
                ws.Cell(fila, 3).Style.Font.FontName = "Arial";
                ws.Cell(fila, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                fila++;
                impar = !impar;
            }

            ws.Column(2).AdjustToContents();
            ws.Column(3).Width = 14;
        }

        private static bool EsPropiedadSimple(PropertyInfo p)
        {
            var t = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
            return t.IsPrimitive
                || t == typeof(string)
                || t == typeof(decimal)
                || t == typeof(DateTime)
                || t == typeof(bool)
                || t == typeof(Guid)
                || t.IsEnum;
        }
    }
}
