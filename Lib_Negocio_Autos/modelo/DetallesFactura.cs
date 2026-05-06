using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class DetallesFactura
    {
		public int Id { get; set; }
		public double? Subtotal { get; set; }
		public string? Descripcion { get; set; }
		public string? TipoFactura { get; set; }
		public int FacturaId { get; set; }
		public Facturas? Factura { get; set; }
       
    }
}
