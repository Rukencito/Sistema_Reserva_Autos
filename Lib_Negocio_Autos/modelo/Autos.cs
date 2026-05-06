using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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

		public int Parqueadero { get; set; }
		public int Dueno { get; set; }
		public int Sucursal { get; set; }
		public int Inventario { get; set; }
		public int Venta { get; set; }

        [ForeignKey("Parqueadero")] public Parqueaderos? _Parqueadero { get; set; }
        [ForeignKey("Dueno")] public Duenos? _Dueno { get; set; }
        [ForeignKey("Sucursal")] public Sucursales? _Sucursal { get; set; }
        [ForeignKey("Inventario")] public Inventarios? _Inventario { get; set; }
        [ForeignKey("Venta")] public Ventas? _Venta { get; set; }
        [NotMapped] public List<Alquileres>? Alquiler { get; set; }
        [NotMapped] public List<Garantias>? Garantia { get; set; }
        [NotMapped] public List<Mantenimientos>? Mantenimiento { get; set; }
        [NotMapped] public List<Reservas>? Reserva { get; set; }
        [NotMapped] public List<Seguros>? Seguro { get; set; }

    }
}
