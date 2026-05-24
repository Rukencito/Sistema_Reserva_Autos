using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IUsuariosNegocio
    {
        List<Usuarios> Consultar();
        Usuarios Guardar(Usuarios entidad);
        Usuarios Eliminar(Usuarios entidad);
        Usuarios Modificar(Usuarios entidad);
        bool ValidarId(int id);
        Usuarios ConsultarPorCorreo(string correo);
        Usuarios AsignarRol(int usuarioId, int rolId);
        List<Usuarios> ConsultarPorRol(int rolId);
        void ValidarDatos(Usuarios entidad);
    }
}
