using Lib_Negocio_Autos.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface ITalleresPresentacion
    {
        List<Talleres> Consultar();
        Talleres Guardar(Talleres entidad);
        Talleres Modificar(Talleres entidad);
        Talleres Eliminar(Talleres entidad);
    }
}
