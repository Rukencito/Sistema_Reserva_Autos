using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class Clientes
    {
		public int Id { get; set; }
		public bool EstadoPago { get; set; }
		public bool LicenciaConduccion { get; set; }
		public int PuntosFidelidad { get; set; }
	    public int Persona { get; set; }
        [ForeignKey("Persona")] public Personas? _Persona { get; set; }

        [NotMapped] public List<Alquileres>? Alquiler { get; set; }

        [NotMapped] public List<Facturas>? Factura { get; set; }

        [NotMapped] public List<Reseñas>? Reseña { get; set; }

        [NotMapped] public List<Reservas>? Reserva { get; set; }
        [NotMapped] public List<Clientes>? Cliente { get; set; }

    }
}
