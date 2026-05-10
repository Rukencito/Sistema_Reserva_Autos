using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Empleados
    {
        public int Id { get; set; }
        public string? Cargo { get; set; }
        public string? Horario { get; set; }
        public double? Salario { get; set; }
        public double Bonificaciones { get; set; }
        [ForeignKey("Personas")] public Personas? _Personas { get; set; }
        [ForeignKey("Sucursales")] public Sucursales? _Sucursales { get; set; }
        [NotMapped] public List<Alquileres>? Alquiler { get; set; }
        [NotMapped] public List<Ventas>? Venta { get; set; }

    }
}
