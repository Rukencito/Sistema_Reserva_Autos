using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Lib_Negocio_Autos.modelo
{
    public class Talleres
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public int Capacidad { get; set; }

        [JsonIgnore]
        [NotMapped] public List<Mantenimientos>? Mantenimientos { get; set; }

    }
}
