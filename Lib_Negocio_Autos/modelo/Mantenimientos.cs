using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Mantenimientos
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string? Tipo { get; set; }
        public string? Descripcion { get; set; }
        public decimal Costo { get; set; }

        public int? Autos { get; set; }
        public int? Talleres { get; set; }

        [ForeignKey("Autos")] public Autos? Auto { get; set; }
        [ForeignKey("Talleres")] public Talleres? Taller { get; set; }

    }
}
