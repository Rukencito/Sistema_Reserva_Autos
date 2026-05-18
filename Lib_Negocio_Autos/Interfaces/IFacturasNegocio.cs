using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IFacturasNegocio
    {
        List<Facturas> Consultar();
        Facturas Guardar(Facturas entidad);
        Facturas Eliminar(Facturas entidad);
        Facturas Modificar(Facturas entidad);
    }
}
