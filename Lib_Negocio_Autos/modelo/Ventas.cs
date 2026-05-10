using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Ventas
    {
        public int Id { get; set; }
        public DateTime FechaVenta { get; set; }
        public double PrecioVenta { get; set; }
        public string? TipoPago { get; set; }
        public bool EstadoPago { get; set; }

        [ForeignKey("Clientes")] public Clientes? _Clientes { get; set; }
        [ForeignKey("Empleados")] public Empleados? _Empleados { get; set; }

        [NotMapped] public List<Autos>? Auto { get; set; }
        [NotMapped] public List<Promociones>? Promocion { get; set; }

    }
}
