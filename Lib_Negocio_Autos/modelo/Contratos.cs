using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class Contratos
    {
		public int Id { get; set; }
		public string? TipoContrato { get; set; }
		public DateTime FechaInicio { get; set; }

		public DateTime FechaFin { get; set; }

		public string? Descripcion { get; set; }

		public int AlquilerId { get; set; }
		public Alquileres? Alquiler { get; set; }

        
    }
}
