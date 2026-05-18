using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IReseñasNegocio
    {
        List<Reseñas> Consultar();
        Reseñas Guardar(Reseñas entidad);
        Reseñas Eliminar(Reseñas entidad);
        Reseñas Modificar(Reseñas entidad);
    }
}
