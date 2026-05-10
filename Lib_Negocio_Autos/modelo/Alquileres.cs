using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Alquileres
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public double PrecioAlquiler { get; set; }
        public bool EstadoAlquiler { get; set; }

        [ForeignKey("Autos")] public Autos? _Autos { get; set; }
        [ForeignKey("Clientes")] public Clientes? _Clientes { get; set; }
        [ForeignKey("Empleados")] public Empleados? _Empleados { get; set; }

        [NotMapped] public List<Contratos>? Contrato { get; set; }

    }
}
