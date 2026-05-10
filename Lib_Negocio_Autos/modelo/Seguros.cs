using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Seguros
    {
        public int Id { get; set; }
        public bool Estado { get; set; }
        public string? Tipo { get; set; }
        public string? Cobertura { get; set; }
        public string? Aseguradora { get; set; }

        [ForeignKey("Autos")] public Autos? _Autos { get; set; }

    }
}
