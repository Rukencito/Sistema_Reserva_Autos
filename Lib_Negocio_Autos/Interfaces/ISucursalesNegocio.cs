using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface ISucursalesNegocio
    {
        List<Sucursales> Consultar();
        Sucursales Guardar(Sucursales entidad);
        Sucursales Eliminar(Sucursales entidad);
        Sucursales Modificar(Sucursales entidad);
    }
}
