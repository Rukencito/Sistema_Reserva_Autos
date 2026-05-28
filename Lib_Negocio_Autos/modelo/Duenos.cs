using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Lib_Negocio_Autos.modelo
{
    public class Duenos //: Personas
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Cedula { get; set; }
        public int Edad { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public int? CantidadAutos { get; set; }
        public bool Estado { get; set; }


        [JsonIgnore]
        [NotMapped] public List<Autos>? Autos { get; set; }
        [JsonIgnore]
        [NotMapped] public List<Usuarios>? Usuario { get; set; }

    }
}
