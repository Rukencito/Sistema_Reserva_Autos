using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface ITalleresNegocio
    {
        List<Talleres> Consultar();
        Talleres Guardar(Talleres entidad);
        Talleres Eliminar(Talleres entidad);
        Talleres Modificar(Talleres entidad);
    }
}
