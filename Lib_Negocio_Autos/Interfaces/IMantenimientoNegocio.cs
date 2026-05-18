using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IMantenimientosNegocio
    {
        List<Mantenimientos> Consultar();
        Mantenimientos Guardar(Mantenimientos entidad);
        Mantenimientos Eliminar(Mantenimientos entidad);
        Mantenimientos Modificar(Mantenimientos entidad);
    }
}
