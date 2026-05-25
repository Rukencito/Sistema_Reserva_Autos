using Lib_Negocio_Autos.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface ISegurosPresentacion
    {
        List<Seguros> Consultar();
        Seguros Guardar(Seguros entidad);
        Seguros Modificar(Seguros entidad);
        Seguros Eliminar(Seguros entidad);
        Seguros ConsultarPorId(int id);

    }
}
