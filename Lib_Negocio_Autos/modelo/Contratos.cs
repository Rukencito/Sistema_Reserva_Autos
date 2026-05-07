using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

		public int Alquiler { get; set; }
        [ForeignKey("Alquiler")] public Alquileres? _Alquiler { get; set; }

        
    }
}
