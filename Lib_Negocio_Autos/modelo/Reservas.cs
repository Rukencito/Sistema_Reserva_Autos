using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Reservas
    {
        public int Id { get; set; }
        public string? FechaReserva { get; set; }
        public string? EstadoReserva { get; set; }
        public double? Anticipo { get; set; }
        public DateTime FechaVencimiento { get; set; }

        [ForeignKey("Autos")] public Autos? _Autos { get; set; }
        [ForeignKey("Clientes")] public Clientes? _Clientes { get; set; }

    }
}
