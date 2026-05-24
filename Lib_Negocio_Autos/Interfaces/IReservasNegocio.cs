using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IReservasNegocio
    {
        List<Reservas> Consultar();
        Reservas Guardar(Reservas entidad);
        Reservas Eliminar(Reservas entidad);
        Reservas Modificar(Reservas entidad);
        bool ValidarId(int id);
        bool ValidarReservaDuplicada(int autoId, int clienteId, DateTime fechaVencimiento);
        Reservas CambiarEstado(int reservaId, string nuevoEstado);
        List<Reservas> ConsultarPorCliente(int clienteId);
        void ValidarDatos(Reservas entidad);
    }
}
