using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IPagosNegocio
    {
        List<Pagos> Consultar();
        Pagos Guardar(Pagos entidad);
        Pagos Eliminar(Pagos entidad);
        Pagos Modificar(Pagos entidad);
    }
}
