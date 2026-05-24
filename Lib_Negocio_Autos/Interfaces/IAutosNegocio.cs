using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IAutosNegocio
    {
        List<Autos> Consultar();
        Autos Guardar(Autos entidad);
        Autos Eliminar(Autos entidad);
        Autos Modificar(Autos entidad);
        bool ValidarPlaca(string placa);
        Autos ConsultarPorPlaca(string placa);
        List<Autos> ConsultarPorMarca(string marca);
        List<Autos> ConsultarPorModelo(string modelo);
        List<Autos> ConsultarDisponibles();
        bool VerificarDisponibilidad(string placa);
        bool CambiarEstado(string placa, bool nuevoEstado);
        void ValidarDatos(Autos entidad);
    }
}
