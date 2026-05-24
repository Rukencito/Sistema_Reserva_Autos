using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_Negocio_Autos.modelo
{
    public class Clientes //: Personas
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Cedula { get; set; }
        public int Edad { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public bool LicenciaConduccion { get; set; }
        public int? PuntosFidelidad { get; set; }
        

        [NotMapped] public List<Alquileres>? Alquiler { get; set; }

        [NotMapped] public List<Facturas>? Factura { get; set; }

        [NotMapped] public List<Resenas>? Resena { get; set; }

        [NotMapped] public List<Reservas>? Reserva { get; set; }

    }
}
