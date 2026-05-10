using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class DetallesFactura
    {
        public int Id { get; set; }
        public double? Subtotal { get; set; }
        public string? Descripcion { get; set; }
        public string? TipoFactura { get; set; }

        [ForeignKey("Facturas")] public Facturas? _Facturas { get; set; }

    }
}
