using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Promociones
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Descuento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public int? Ventas { get; set; }

        [ForeignKey("Ventas")] public Ventas? Venta { get; set; }


    }
}
