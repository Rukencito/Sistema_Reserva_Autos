using Lib_Negocio_Autos.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface IAutosPresentacion
    {
        List<Autos> Consultar();
        Autos Guardar(Autos entidad);
        Autos Modificar(Autos entidad);
        Autos Eliminar(Autos entidad);
    }
}
