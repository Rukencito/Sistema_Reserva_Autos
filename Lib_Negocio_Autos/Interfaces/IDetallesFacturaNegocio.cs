using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IDetallesFacturaNegocio
    {
        List<DetallesFactura> Consultar();
        DetallesFactura Guardar(DetallesFactura entidad);
        DetallesFactura Eliminar(DetallesFactura entidad);
        DetallesFactura Modificar(DetallesFactura entidad);
    }
}
