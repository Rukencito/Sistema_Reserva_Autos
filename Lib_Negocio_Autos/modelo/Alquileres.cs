using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Lib_Negocio_Autos.modelo
{
    public class Alquileres
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal PrecioAlquiler { get; set; }
        public bool EstadoAlquiler { get; set; }

        public int Autos { get; set; }
        public int Clientes { get; set; }
        public int Empleados { get; set; }

        [ForeignKey("Autos")] public Autos? Auto { get; set; }
        [ForeignKey("Clientes")] public Clientes? Cliente { get; set; }
        [ForeignKey("Empleados")] public Empleados? Empleado { get; set; }

        [JsonIgnore]
        [NotMapped] public List<Contratos>? Contrato { get; set; }

    }
}
