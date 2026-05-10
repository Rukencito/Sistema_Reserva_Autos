using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Devoluciones
    {
        public int Id { get; set; }
        public DateTime FechaEntrega { get; set; }
        public int NivelCombustible { get; set; }
        public string? Observaciones { get; set; }

        [ForeignKey("Alquileres")] public Alquileres? _Alquileres { get; set; }
    }
}
