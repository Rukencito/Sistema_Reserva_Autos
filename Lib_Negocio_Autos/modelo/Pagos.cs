using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class Pagos
    {
		public int Id { get; set; }
		public double? Monto { get; set; }
		public bool EstadoPago { get; set; }
		public string? MetodoPago { get; set; }
		public DateTime FechaPago { get; set; }

		public int FacturaId { get; set; }
		public Facturas? Factura { get; set; }

    }
}
