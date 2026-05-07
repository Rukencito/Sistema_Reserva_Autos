using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Promociones
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public double? Descuento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Venta { get; set; }
        [ForeignKey("Venta")] public Ventas? _Venta { get; set; }


    }
}
