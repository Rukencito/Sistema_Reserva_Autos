using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface ISucursalesNegocio
    {
        List<Sucursales> Consultar();
        Sucursales Guardar(Sucursales entidad);
        Sucursales Eliminar(Sucursales entidad);
        Sucursales Modificar(Sucursales entidad);
        bool ValidarId(int id);
        List<Sucursales> ConsultarPorCiudad(string ciudad);
    }
}
