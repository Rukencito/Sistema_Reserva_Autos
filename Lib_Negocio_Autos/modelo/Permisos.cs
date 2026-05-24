using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Permisos
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        public int Roles { get; set; }

        [ForeignKey("Roles")] public Roles? Rol { get; set; }

    }
}

