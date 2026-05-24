using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Empleados //: Personas
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Cedula { get; set; }
        public int Edad { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Cargo { get; set; }
        public string? Horario { get; set; }
        public decimal Salario { get; set; }
        public decimal? Bonificaciones { get; set; }

        public int? Sucursales { get; set; }

        [ForeignKey("Sucursales")] public Sucursales? Sucursal { get; set; }
        [NotMapped] public List<Alquileres>? Alquiler { get; set; }
        [NotMapped] public List<Ventas>? Venta { get; set; }

    }
}
