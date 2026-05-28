using System.ComponentModel.DataAnnotations.Schema;

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

        public int? Roles { get; set; }
        public int? Clientes { get; set; }
        public int? Empleados { get; set; }
        public int? Duenos { get; set; }

        [ForeignKey("Roles")] public Roles? Rol { get; set; }
        [ForeignKey("Clientes")] public Clientes? Cliente { get; set; }
        [ForeignKey("Empleados")] public Empleados? Empleado { get; set; }
        [ForeignKey("Duenos")] public Duenos? Dueno { get; set; }

    }
}
