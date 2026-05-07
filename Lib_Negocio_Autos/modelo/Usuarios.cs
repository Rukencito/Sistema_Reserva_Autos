using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class Usuarios
    {
		public int Id { get; set; }
		public string? Nombre { get; set; }
		public string? Apellido { get; set; }
		public string? Correo { get; set; }
		public string? Contraseña { get; set; }
		public string? Telefono { get; set; }
		public int Rol { get; set; }
        [ForeignKey("Rol")] public Roles? _Rol { get; set; }

    }
}
