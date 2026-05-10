using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Duenos
    {
        public int Id { get; set; }
        public int CantidadAutos { get; set; }
        public bool Estado { get; set; }
        [ForeignKey("Personas")] public Personas? _Personas { get; set; }

        [NotMapped] public List<Autos>? Autos { get; set; }

    }
}
