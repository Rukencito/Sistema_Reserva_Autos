using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IFacturasNegocio
    {
        List<Facturas> Consultar();
        Facturas Guardar(Facturas entidad);
        Facturas Eliminar(Facturas entidad);
        Facturas Modificar(Facturas entidad);
        bool ValidarId(int id);
        decimal CalcularTotal(Facturas factura);
        List<Facturas> ConsultarPorCliente(int clienteId);
        Facturas ConsultarPorId(int id);
        List<Facturas> ConsultarPendientes();
        Facturas MarcarComoPagada(int facturaId);
        void ValidarDatos(Facturas entidad);
    }
}
