using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface ISegurosNegocio
    {
        List<Seguros> Consultar();
        Seguros Guardar(Seguros entidad);
        Seguros Eliminar(Seguros entidad);
        Seguros Modificar(Seguros entidad);
    }
}
