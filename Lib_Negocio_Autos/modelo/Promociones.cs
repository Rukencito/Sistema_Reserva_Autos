using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class Promociones
    {
		public int Id { get; set; }
		public string? Descripcion { get; set; }
		public double? Descuento { get; set; }
		public DateTime FechaInicio	{ get; set; }
		public DateTime FechaFin { get; set; }
		public int VentaId { get; set; }
		public Ventas? Venta { get; set; }

       
    }
}
