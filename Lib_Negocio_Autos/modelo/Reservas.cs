using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

		public int Auto { get; set; }
		public int Cliente { get; set; }

        [ForeignKey("Auto")] public Autos? _Auto { get; set; }
        [ForeignKey("Cliente")] public Clientes? _Cliente { get; set; }
     
    }
}
