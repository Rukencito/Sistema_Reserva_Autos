using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class Alquileres
    {
		public int Id { get; set; }
		public DateTime FechaInicio { get; set; }
		public DateTime FechaFin { get; set; }
		public double PrecioAlquiler { get; set; }

		public bool EstadoAlquiler { get; set; }

		public int Auto { get; set; }
		public int Cliente { get; set; }
		public int Empleado { get; set; }
        [ForeignKey("Auto")] public Autos? _Auto { get; set; }
        [ForeignKey("Cliente")] public Clientes? _Cliente { get; set; }

        [ForeignKey("Empleado")] public Empleados? _Empleado { get; set; }

        [NotMapped] public List<Contratos>? Contrato { get; set; }

    }
}
