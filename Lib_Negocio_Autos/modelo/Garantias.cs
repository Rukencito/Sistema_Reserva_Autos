using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Garantias
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Auto { get; set; }
        [ForeignKey("Auto")] public Autos? _Auto { get; set; }

    }
}
