using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Lib_Negocio_Autos.modelo
{
    public class Roles
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public bool Estado { get; set; }

        [JsonIgnore]
        [NotMapped] public List<Usuarios>? Usuario { get; set; }

    }
}
