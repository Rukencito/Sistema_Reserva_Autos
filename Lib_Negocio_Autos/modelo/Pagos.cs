using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Pagos
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public bool EstadoPago { get; set; }
        public string? MetodoPago { get; set; }
        public DateTime FechaPago { get; set; }

        public int? Facturas { get; set; }
        [ForeignKey("Facturas")] public Facturas? Factura { get; set; }

    }
}
