using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class Facturas
    {
		public int Id { get; set; }
		public double? Total { get; set; }
		public DateTime FechaEmision { get; set; }
		public double? IVA	{ get; set; }
		public bool Estado { get; set; }

		public int ClienteId { get; set; }
		public Clientes? Cliente { get; set; }

    }
}
