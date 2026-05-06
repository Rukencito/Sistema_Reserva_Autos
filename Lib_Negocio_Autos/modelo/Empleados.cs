using System;
using System.Collections.Generic;
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
		public int PersonaId { get; set; }
		public int SucursalId { get; set; }
        public Personas? Persona { get; set; }
		public Sucursales? Sucursal { get; set; }
       
    }
}
