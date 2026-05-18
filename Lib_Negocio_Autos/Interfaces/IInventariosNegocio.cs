using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IInventariosNegocio
    {
        List<Inventarios> Consultar();
        Inventarios Guardar(Inventarios entidad);
        Inventarios Eliminar(Inventarios entidad);
        Inventarios Modificar(Inventarios entidad);
    }
}
