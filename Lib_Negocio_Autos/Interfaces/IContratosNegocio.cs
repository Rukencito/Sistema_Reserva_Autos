using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IContratosNegocio
    {
        List<Contratos> Consultar();
        Contratos Guardar(Contratos entidad);
        Contratos Eliminar(Contratos entidad);
        Contratos Modificar(Contratos entidad);
    }
}
