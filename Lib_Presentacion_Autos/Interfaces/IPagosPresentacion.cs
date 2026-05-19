using Lib_Negocio_Autos.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface IPagosPresentacion
    {
        List<Pagos> Consultar();
        Pagos Guardar(Pagos entidad);
        Pagos Modificar(Pagos entidad);
        Pagos Eliminar(Pagos entidad);
    }
}
