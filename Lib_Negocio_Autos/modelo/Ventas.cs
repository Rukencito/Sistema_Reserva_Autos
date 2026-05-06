using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class Ventas
    {
		public int Id { get; set; }
		public DateTime FechaVenta { get; set; }
		public double PrecioVenta { get; set; }
		public string? TipoPago { get; set; }
		public bool EstadoPago  { get; set; }

		public int ClienteId { get; set; }
		public int EmpleadoId { get; set; }

		public Clientes? Cliente { get; set; }
		public Empleados? Empleado { get; set; }

    }
}
