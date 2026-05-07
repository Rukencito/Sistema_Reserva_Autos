using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Mantenimientos
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string? Tipo { get; set; }
        public string? Descripcion { get; set; }
        public double Costo { get; set; }
        public int Auto { get; set; }
        public int Taller { get; set; }
        [ForeignKey("Auto")] public Autos? _Auto { get; set; }
        [ForeignKey("Taller")] public Talleres? _Taller { get; set; }

    }
}
