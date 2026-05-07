using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Devoluciones
    {
        public int Id { get; set; }
        public DateTime FechaEntrega { get; set; }

        public int NivelCombustible { get; set; }
        public string? Descripcion { get; set; }
        public int Alquiler { get; set; }
        [ForeignKey("Alquiler")] public Alquileres? _Alquiler { get; set; }
    }
}
