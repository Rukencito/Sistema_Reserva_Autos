using Lib_Negocio_Autos.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IPersonasNegocio
    {
        List<Personas> Consultar();
        Personas Guardar(Personas entidad);
    }
}
