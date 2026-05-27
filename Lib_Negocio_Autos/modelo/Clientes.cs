using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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


        [JsonIgnore]
        [NotMapped] public List<Alquileres>? Alquiler { get; set; }
        [JsonIgnore]
        [NotMapped] public List<Facturas>? Factura { get; set; }
        [JsonIgnore]
        [NotMapped] public List<Resenas>? Resena { get; set; }
        [JsonIgnore]
        [NotMapped] public List<Reservas>? Reserva { get; set; }

    }
}
