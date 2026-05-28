using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Lib_Negocio_Autos.modelo
{
    public class Ventas
    {
        public int Id { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal PrecioVenta { get; set; }
        public string? TipoPago { get; set; }
        public bool EstadoPago { get; set; }

        public int? Clientes { get; set; }
        public int? Empleados { get; set; }
        public int? Autos { get; set; }

        [JsonIgnore]
        [ForeignKey("Clientes")] public Clientes? Cliente { get; set; }
        [JsonIgnore]
        [ForeignKey("Empleados")] public Empleados? Empleado { get; set; }
        [JsonIgnore]
        [ForeignKey("Autos")] public Autos? Auto { get; set; }

        [NotMapped] public List<Promociones>? Promocion { get; set; }

    }
}
