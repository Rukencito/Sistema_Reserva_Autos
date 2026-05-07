using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Roles
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public bool Estado { get; set; }

        [NotMapped] public List<Usuarios>? Usuario { get; set; }

    }
}
