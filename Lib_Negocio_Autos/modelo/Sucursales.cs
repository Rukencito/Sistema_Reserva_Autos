using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Sucursales
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Ciudad { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }

        [NotMapped] public List<Autos>? Auto { get; set; }

    }
}
