using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class Empleados
    {
		public int Id { get; set; }
		public string? Cargo { get; set; }
		public string? Horario { get; set; }
		public double? Salario { get; set; }
		public double Bonificaciones { get; set; }
		public int Persona { get; set; }
		public int Sucursal { get; set; }
        [ForeignKey("Persona")] public Personas? _Persona { get; set; }
        [ForeignKey("Sucursal")] public Sucursales? _Sucursal { get; set; }
        [NotMapped] public List<Alquileres>? Alquiler { get; set; }
        [NotMapped] public List<Ventas>? Venta { get; set; }

    }
}
