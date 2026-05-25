using Lib_Negocio_Autos.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface IDevolucionesPresentacion
    {
        List<Devoluciones> Consultar();
        Devoluciones Guardar(Devoluciones entidad);
        Devoluciones Modificar(Devoluciones entidad);
        Devoluciones Eliminar(Devoluciones entidad);
        Devoluciones ConsultarPorAlquiler(int idAlquiler);
        Devoluciones ConsultarPorId(int id);

    }
}
