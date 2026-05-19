using Lib_Negocio_Autos.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface IPersonasPresentacion
    {
        List<Personas> Consultar();
        Personas Guardar(Personas entidad);
        Personas Modificar(Personas entidad);
        Personas Eliminar(Personas entidad);
    }
}
