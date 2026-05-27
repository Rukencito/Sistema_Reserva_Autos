using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Lib_Negocio_Autos.modelo
{
    public class Inventarios
    {
        public int Id { get; set; }
        public int? Cantidad { get; set; }
        public string? Ubicacion { get; set; }
        public decimal? ValorTotal { get; set; }
        public DateTime FechaActualizacion { get; set; }

        [JsonIgnore]
        [NotMapped] public List<Autos>? Auto { get; set; }
    }
}
