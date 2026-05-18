using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IParqueaderosNegocio
    {
        List<Parqueaderos> Consultar();
        Parqueaderos Guardar(Parqueaderos entidad);
        Parqueaderos Eliminar(Parqueaderos entidad);
        Parqueaderos Modificar(Parqueaderos entidad);
    }
}
