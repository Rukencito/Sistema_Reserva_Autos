using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IParqueaderosNegocio
    {
        List<Parqueaderos> Consultar();
        Parqueaderos Guardar(Parqueaderos entidad);
        Parqueaderos Eliminar(Parqueaderos entidad);
        Parqueaderos Modificar(Parqueaderos entidad);
        bool ValidarId(int id);
        Parqueaderos ConsultarPorId(int id);
        int ContarAutosEnParqueadero(int parqueaderoId);
        int ConsultarEspaciosDisponibles(int parqueaderoId);
        bool TieneEspacioDisponible(int parqueaderoId);
        List<Parqueaderos> ConsultarConEspacioDisponible();
        List<Autos> ConsultarAutosPorParqueadero(int parqueaderoId);
        void ValidarDatos(Parqueaderos entidad);
    }
}
