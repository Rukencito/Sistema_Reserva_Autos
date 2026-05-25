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
        Parqueaderos ConsultarPorId(int id);
        int ContarAutosEnParqueadero(int parqueaderoId);
        int ConsultarEspaciosDisponibles(int parqueaderoId);
        bool TieneEspacioDisponible(int parqueaderoId);
        List<Parqueaderos> ConsultarConEspacioDisponible();
        List<Autos> ConsultarAutosPorParqueadero(int parqueaderoId);


    }
}
