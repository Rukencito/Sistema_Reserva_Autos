using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IDevolucionesNegocio
    {
        List<Devoluciones> Consultar();
        Devoluciones Guardar(Devoluciones entidad);
        Devoluciones Eliminar(Devoluciones entidad);
        Devoluciones Modificar(Devoluciones entidad);
    }
}
