using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IAutosNegocio
    {
        List<Autos> Consultar();
        Autos Guardar(Autos entidad);
        Autos Eliminar(Autos entidad);
        Autos Modificar(Autos entidad);
    }
}
