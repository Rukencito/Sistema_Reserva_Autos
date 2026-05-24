using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class DetallesFactura
    {
        public int Id { get; set; }
        public decimal? Subtotal { get; set; }
        public string? Descripcion { get; set; }
        public string? TipoFactura { get; set; }

        public int Facturas { get; set; }

        [ForeignKey("Facturas")] public Facturas? Factura { get; set; }

    }
}
