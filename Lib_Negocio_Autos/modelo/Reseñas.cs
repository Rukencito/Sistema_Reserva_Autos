using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class Reseñas
    {
		public int Id { get; set; }
		public DateTime Fecha { get; set; }
		public int Calificacion { get; set; }
		public string? Comentario { get; set; }
		public string? TipoServicio { get; set; }
		public int ClienteId { get; set; }
		public Clientes? Cliente { get; set; }

      
    }
}
