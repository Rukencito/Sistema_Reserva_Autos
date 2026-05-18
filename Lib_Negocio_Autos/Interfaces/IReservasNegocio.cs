using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IReservasNegocio
    {
        List<Reservas> Consultar();
        Reservas Guardar(Reservas entidad);
        Reservas Eliminar(Reservas entidad);
        Reservas Modificar(Reservas entidad);
    }
}
