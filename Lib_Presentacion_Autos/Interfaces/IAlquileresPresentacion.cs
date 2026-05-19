using Lib_Negocio_Autos.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface IAlquileresPresentacion
    {
        List<Alquileres> Consultar();
        Alquileres Guardar(Alquileres entidad);
        Alquileres Modificar(Alquileres entidad);
        Alquileres Eliminar(Alquileres entidad);
    }
}
