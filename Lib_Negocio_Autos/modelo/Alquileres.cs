using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class Alquileres
    {
		public int Id { get; set; }
		public DateTime FechaInicio { get; set; }
		public DateTime FechaFin { get; set; }
		public double PrecioAlquiler { get; set; }

		public bool EstadoAlquiler { get; set; }

		public int AutoId { get; set; }
		public int ClienteId { get; set; }
		public int EmpleadoId { get; set; }
		public Autos? Auto { get; set; }
		public Clientes? Cliente { get; set; }

		public Empleados? Empleado { get; set; }
       
    }
}
