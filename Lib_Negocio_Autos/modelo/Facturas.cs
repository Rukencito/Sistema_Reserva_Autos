using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Facturas
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaEmision { get; set; }
        public bool Estado { get; set; }

        public int Clientes { get; set; }

        [ForeignKey("Clientes")] public Clientes? Cliente { get; set; }

        [NotMapped] public List<DetallesFactura>? DetalleFactura { get; set; }
        [NotMapped] public List<Pagos>? Pago { get; set; }

    }
}
