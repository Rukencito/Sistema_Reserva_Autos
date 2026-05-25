using Lib_Negocio_Autos.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface IPermisosPresentacion
    {
        List<Permisos> Consultar();
        Permisos Guardar(Permisos entidad);
        Permisos Modificar(Permisos entidad);
        Permisos Eliminar(Permisos entidad);
        bool TienePermiso(int usuarioId, string nombrePermiso);
        bool TienePermisoPorCorreo(string correo, string nombrePermiso);
        bool PermisoExisteEnRol(string nombrePermiso, int rolId);

    }
}
