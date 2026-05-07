using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Facturas
    {
        public int Id { get; set; }
        public double? Total { get; set; }
        public DateTime FechaEmision { get; set; }
        public double? IVA { get; set; }
        public bool Estado { get; set; }

        public int Cliente { get; set; }
        [ForeignKey("Cliente")] public Clientes? _Cliente { get; set; }

        [NotMapped] public List<DetallesFactura>? DetalleFactura { get; set; }
        [NotMapped] public List<Pagos>? Pago { get; set; }

    }
}
