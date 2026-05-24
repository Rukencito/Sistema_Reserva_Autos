using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Devoluciones
    {
        public int Id { get; set; }
        public DateTime FechaEntrega { get; set; }
        public int NivelCombustible { get; set; }
        public int Kilometraje { get; set; }
        public string? Observaciones { get; set; }

        public int Alquileres { get; set; }

        [ForeignKey("Alquileres")] public Alquileres? Alquiler { get; set; } 
    }

}
