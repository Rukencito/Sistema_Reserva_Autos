using Lib_Negocio_Autos.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface IContratosPresentacion
    {
        List<Contratos> Consultar();
        Contratos Guardar(Contratos entidad);
        Contratos Modificar(Contratos entidad);
        Contratos Eliminar(Contratos entidad);
        Contratos ConsultarPorId(int id);
        List<Contratos> ConsultarPorAlquiler(int alquilerId);

    }
}
