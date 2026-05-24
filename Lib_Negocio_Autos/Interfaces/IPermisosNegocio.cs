using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IPermisosNegocio
    {
        List<Permisos> Consultar();
        Permisos Guardar(Permisos entidad);
        Permisos Eliminar(Permisos entidad);
        Permisos Modificar(Permisos entidad);
        bool TienePermiso(int usuarioId, string nombrePermiso);
        bool TienePermisoPorCorreo(string correo, string nombrePermiso);
        bool PermisoExisteEnRol(string nombrePermiso, int rolId);
        void ValidarDatos(Permisos entidad);
        bool ValidarId(int id);
    }
}
