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

        [ForeignKey("Clientes")] public Clientes? _Clientes { get; set; }

        [NotMapped] public List<DetallesFactura>? DetalleFactura { get; set; }
        [NotMapped] public List<Pagos>? Pago { get; set; }

    }
}
