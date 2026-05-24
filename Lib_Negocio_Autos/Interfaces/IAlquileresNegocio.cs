using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IAlquileresNegocio
    {
        List<Alquileres> Consultar();
        Alquileres Guardar(Alquileres entidad);
        Alquileres Eliminar(Alquileres entidad);
        Alquileres Modificar(Alquileres entidad);
        bool ValidarId(int id);
        List<Alquileres> ConsultarEstadoAlquiler(bool estadoAlquiler);
        List<Alquileres> ConsultarAlquileresPorCliente(int clienteId);
        bool ExisteCruceDeFechas(int autoId, DateTime fechaInicio, DateTime fechaFin);
        decimal CalcularTotalPrecio(decimal precioAlquiler, DateTime fechaInicio, DateTime fechaFin);
        void ValidarDatos(Alquileres entidad);
    }
}
