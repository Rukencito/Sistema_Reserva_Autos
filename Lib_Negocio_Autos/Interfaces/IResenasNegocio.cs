using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IResenasNegocio
    {
        List<Resenas> Consultar();
        Resenas Guardar(Resenas entidad);
        Resenas Eliminar(Resenas entidad);
        Resenas Modificar(Resenas entidad);
        bool ValidarId(int id);
        void ValidarDatos(Resenas entidad);
        List<Resenas> ConsultarPorCliente(int idCliente);
    }
}
