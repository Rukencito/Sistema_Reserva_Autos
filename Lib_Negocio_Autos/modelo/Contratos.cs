using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Contratos
    {
        public int Id { get; set; }
        public string? TipoContrato { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string? Descripcion { get; set; }

        [ForeignKey("Alquileres")] public Alquileres? _Alquileres { get; set; }


    }
}
