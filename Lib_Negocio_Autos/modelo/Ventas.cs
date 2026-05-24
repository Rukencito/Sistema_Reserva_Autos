using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("Clientes")] public Clientes? Cliente { get; set; }
        [ForeignKey("Empleados")] public Empleados? Empleado { get; set; }

        [NotMapped] public List<Autos>? Auto { get; set; }
        [NotMapped] public List<Promociones>? Promocion { get; set; }

    }
}
