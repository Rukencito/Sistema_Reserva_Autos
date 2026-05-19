using Lib_Negocio_Autos.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface IReservasPresentacion
    {
        List<Reservas> Consultar();
        Reservas Guardar(Reservas entidad);
        Reservas Modificar(Reservas entidad);
        Reservas Eliminar(Reservas entidad);
    }
}
