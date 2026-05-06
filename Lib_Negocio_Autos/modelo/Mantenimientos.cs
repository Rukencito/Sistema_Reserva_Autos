using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class Mantenimientos
    {
		public int Id { get; set; }
		public DateTime Fecha { get; set; }
		public string? Tipo { get; set; }
		public string? Descripcion { get; set; }
		public double Costo { get; set; }
		public int AutoId { get; set; }
		public int TallerId { get; set; }
        public Autos? Auto { get; set; }
		public Talleres? Taller { get; set; }

    }
}
