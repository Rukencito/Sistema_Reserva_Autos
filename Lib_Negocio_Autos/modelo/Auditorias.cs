namespace Lib_Negocio_Autos.modelo
{
    public class Auditorias
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaHora { get; set; }
        public string? Usuario { get; set; }
        public string? Accion { get; set; }

    }
}
