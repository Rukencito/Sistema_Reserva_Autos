using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IAlquileresNegocio
    {
        List<Alquileres> Consultar();
        Alquileres Guardar(Alquileres entidad);
        Alquileres Eliminar(Alquileres entidad);
        Alquileres Modificar(Alquileres entidad);
    }
}
