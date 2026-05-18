using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IPersonasNegocio
    {
        List<Personas> Consultar();
        Personas Guardar(Personas entidad);
        Personas Eliminar(Personas entidad);
        Personas Modificar(Personas entidad);
    }
}
