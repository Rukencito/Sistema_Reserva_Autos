using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IDuenosNegocio
    {
        List<Duenos> Consultar();
        Duenos Guardar(Duenos entidad);
        Duenos Eliminar(Duenos entidad);
        Duenos Modificar(Duenos entidad);
        bool ValidarId(int id);
        bool ValidarCedula(string cedula);
        Duenos ConsultarPorCedula(string cedula);
        bool VerificarEstadoDueno(int duenoId);
        Duenos AgregarAuto(int duenoId);
        Duenos QuitarAuto(int duenoId);
        void ValidarDatos(Duenos entidad);
    }
}
