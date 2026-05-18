using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IPermisosNegocio
    {
        List<Permisos> Consultar();
        Permisos Guardar(Permisos entidad);
        Permisos Eliminar(Permisos entidad);
        Permisos Modificar(Permisos entidad);
    }
}
