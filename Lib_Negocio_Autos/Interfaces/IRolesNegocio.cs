using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IRolesNegocio
    {
        List<Roles> Consultar();
        Roles Guardar(Roles entidad);
        Roles Eliminar(Roles entidad);
        Roles Modificar(Roles entidad);
        bool ValidarId(int id);
        bool NombreExiste(string nombre);
        Roles ConsultarPorId(int id);
        void ValidarDatos(Roles entidad);
    }
}
