using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Autos
    {
        public int Id { get; set; }
        public string? Placa { get; set; }
        public string? Marca { get; set; }
        public int Año { get; set; }
        public string? Modelo { get; set; }
        public bool Estado { get; set; }
        public string? Color { get; set; }

        public int? Parqueaderos { get; set; }
        public int? Duenos { get; set; }
        public int? Sucursales { get; set; }
        public int? Inventarios { get; set; }

        [ForeignKey("Parqueaderos")] public Parqueaderos? Parqueadero { get; set; }
        [ForeignKey("Duenos")] public Duenos? Dueno { get; set; }
        [ForeignKey("Sucursales")] public Sucursales? Sucursal { get; set; }
        [ForeignKey("Inventarios")] public Inventarios? Inventario { get; set; }
        [NotMapped] public List<Alquileres>? Alquiler { get; set; }
        [NotMapped] public List<Garantias>? Garantia { get; set; }
        [NotMapped] public List<Mantenimientos>? Mantenimiento { get; set; }
        [NotMapped] public List<Reservas>? Reserva { get; set; }
        [NotMapped] public List<Seguros>? Seguro { get; set; }

    }
}
