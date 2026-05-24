using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IDetallesFacturaNegocio
    {
        List<DetallesFactura> Consultar();
        DetallesFactura Guardar(DetallesFactura entidad);
        DetallesFactura Eliminar(DetallesFactura entidad);
        DetallesFactura Modificar(DetallesFactura entidad);
        bool ValidarId(int id);
        DetallesFactura ConsultarPorId(int id);
        List<DetallesFactura> ConsultarPorFactura(int facturaId);
        decimal CalcularSubtotalPorFactura(int facturaId);
        void ValidarDatos(DetallesFactura entidad);
    }
}
