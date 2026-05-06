using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class Clientes
    {
		public int Id { get; set; }
		public bool EstadoPago { get; set; }
		public string? LicenciaConduccion { get; set; }
		public int PuntosFidelidad { get; set; }
	    public int PersonaId { get; set; }
		public Personas? Persona { get; set; }
   
    }
}
