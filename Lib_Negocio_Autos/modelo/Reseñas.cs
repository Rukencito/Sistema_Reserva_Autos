using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Reseñas
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Calificacion { get; set; }
        public string? Comentario { get; set; }
        public string? TipoServicio { get; set; }
        public int Cliente { get; set; }
        [ForeignKey("Cliente")] public Clientes? _Cliente { get; set; }


    }
}
