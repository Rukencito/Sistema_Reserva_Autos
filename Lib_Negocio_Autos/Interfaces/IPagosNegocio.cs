using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IPagosNegocio
    {
        List<Pagos> Consultar();
        Pagos Guardar(Pagos entidad);
        Pagos Eliminar(Pagos entidad);
        Pagos Modificar(Pagos entidad);
        bool ValidarId(int id);
        Pagos ConsultarPorId(int id);
        List<Pagos> ConsultarPorFactura(int facturaId);
        void ValidarDatos(Pagos entidad);
    }
}
