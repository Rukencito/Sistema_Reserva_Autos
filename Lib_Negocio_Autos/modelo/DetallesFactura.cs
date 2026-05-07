using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class DetallesFactura
    {
        public int Id { get; set; }
        public double? Subtotal { get; set; }
        public string? Descripcion { get; set; }
        public string? TipoFactura { get; set; }
        public int Factura { get; set; }
        [ForeignKey("Factura")] public Facturas? _Factura { get; set; }

    }
}
