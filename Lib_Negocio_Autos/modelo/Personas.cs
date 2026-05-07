using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class Personas
    {
		public int Id { get; set; }
		public string? Nombre { get; set; }
		public string? Apellido { get; set; }
		public int Edad { get; set; }
		public string? Genero { get; set; }
		public string? Correo { get; set; }
		public string? Telefono { get; set; }

		[NotMapped] public List<Clientes>? Cliente { get; set; }
		[NotMapped] public List<Duenos>? Dueno { get; set; }

        [NotMapped] public List<Empleados>? Empleado { get; set; }
    }
}
