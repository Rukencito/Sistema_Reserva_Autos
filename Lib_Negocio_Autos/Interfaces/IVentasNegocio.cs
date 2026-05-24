using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IVentasNegocio
    {
        List<Ventas> Consultar();
        Ventas Guardar(Ventas entidad);
        Ventas Eliminar(Ventas entidad);
        Ventas Modificar(Ventas entidad);
        bool ValidarId(int id);
        List<Ventas>ConsultarPorCliente(int idCliente);
        void ValidarDatos(Ventas entidad);

    }
}
