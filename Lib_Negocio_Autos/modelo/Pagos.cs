using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Pagos
    {
        public int Id { get; set; }
        public double? Monto { get; set; }
        public bool EstadoPago { get; set; }
        public string? MetodoPago { get; set; }
        public DateTime FechaPago { get; set; }

        public int Factura { get; set; }
        [ForeignKey("Factura")] public Facturas? _Factura { get; set; }

    }
}
