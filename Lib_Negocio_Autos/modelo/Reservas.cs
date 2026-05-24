using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Reservas
    {
        public int Id { get; set; }
        public DateTime FechaReserva { get; set; }
        public string? EstadoReserva { get; set; }
        public decimal? Anticipo { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public int? Autos { get; set; }
        public int? Clientes { get; set; }

        [ForeignKey("Autos")] public Autos? Auto { get; set; }
        [ForeignKey("Clientes")] public Clientes? Cliente { get; set; }

    }
}
