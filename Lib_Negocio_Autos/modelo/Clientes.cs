using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Clientes : Personas
    {
        // public int Id { get; set; }
        public bool LicenciaConduccion { get; set; }
        public int PuntosFidelidad { get; set; }
        
        //[ForeignKey("Personas")] public Personas? _Personas { get; set; }

        [NotMapped] public List<Alquileres>? Alquiler { get; set; }

        [NotMapped] public List<Facturas>? Factura { get; set; }

        [NotMapped] public List<Resenas>? Resena { get; set; }

        [NotMapped] public List<Reservas>? Reserva { get; set; }

    }
}
