using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class Inventarios
    {
		public int Id { get; set; }
		public int Cantidad { get; set; }
		public string? Ubicacion { get; set; }
		public double? ValorTotal { get; set; }
		public DateTime FechaActualizacion { get; set; }
     
    }
}
