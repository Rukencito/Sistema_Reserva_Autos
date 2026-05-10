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

        [ForeignKey("Autos")] public Autos? _Autos { get; set; }
        [ForeignKey("Talleres")] public Talleres? _Talleres { get; set; }

    }
}
