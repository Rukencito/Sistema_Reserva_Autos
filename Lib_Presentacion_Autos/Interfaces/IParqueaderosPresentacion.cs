using Lib_Negocio_Autos.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface IParqueaderosPresentacion
    {
        List<Parqueaderos> Consultar();
        Parqueaderos Guardar(Parqueaderos entidad);
        Parqueaderos Modificar(Parqueaderos entidad);
        Parqueaderos Eliminar(Parqueaderos entidad);
    }
}
