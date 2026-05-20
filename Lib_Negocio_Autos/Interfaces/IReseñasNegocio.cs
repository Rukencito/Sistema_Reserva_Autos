using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IReseñasNegocio
    {
        List<Resenas> Consultar();
        Resenas Guardar(Resenas entidad);
        Resenas Eliminar(Resenas entidad);
        Resenas Modificar(Resenas entidad);
    }
}
