using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IInventariosNegocio
    {
        List<Inventarios> Consultar();
        Inventarios Guardar(Inventarios entidad);
        Inventarios Eliminar(Inventarios entidad);
        Inventarios Modificar(Inventarios entidad);
        bool ValidarId(int id);
        Inventarios ConsultarPorId(int id);
        List<Inventarios> ConsultarPorUbicacion(string ubicacion);
        Inventarios AgregarStock(int inventarioId, int cantidad, decimal precioUnitario);
        Inventarios ReducirStock(int inventarioId, int cantidad, decimal precioUnitario);
        Inventarios RecalcularValorTotal(int inventarioId);
        void ValidarDatos(Inventarios entidad);
    }
}
