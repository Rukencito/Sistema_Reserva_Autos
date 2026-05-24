using Lib_Negocio_Autos.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface IResenasPresentacion
    {
        List<Resenas> Consultar();
        Resenas Guardar(Resenas entidad);
        Resenas Modificar(Resenas entidad);
        Resenas Eliminar(Resenas entidad);
    }
}
