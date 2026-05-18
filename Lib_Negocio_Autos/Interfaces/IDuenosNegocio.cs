using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IDuenosNegocio
    {
        List<Duenos> Consultar();
        Duenos Guardar(Duenos entidad);
        Duenos Eliminar(Duenos entidad);
        Duenos Modificar(Duenos entidad);
    }
}
