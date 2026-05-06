using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class Reservas
    {
		public int Id { get; set; }
		public string? FechaReserva { get; set; }
		public string? EstadoReserva { get; set; }
		public double? Anticipo { get; set; }
		public DateTime FechaVencimiento { get; set; }

		public int AutoId { get; set; }
		public int ClienteId { get; set; }

		public Autos? Auto { get; set; }
		public Clientes? Cliente { get; set; }
     
    }
}
