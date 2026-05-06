using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class Seguros
    {
		public int Id { get; set; }
		public bool Estado { get; set; }
		public string? Tipo { get; set; }
		public string? Cobertura { get; set; }
		public string? Aseguradora { get; set; }

		public int AutoId { get; set; }
		public Autos? Auto { get; set; }
        
    }
}
