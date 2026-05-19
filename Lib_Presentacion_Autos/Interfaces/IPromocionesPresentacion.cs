using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface IPromocionesPresentacion
    {
        List<Promociones> Consultar();
        Promociones Guardar(Promociones entidad);
        Promociones Modificar(Promociones entidad);
        Promociones Eliminar(Promociones entidad);
    }
}
