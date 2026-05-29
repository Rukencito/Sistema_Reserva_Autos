using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class RolesNegocio : IRolesNegocio
    {
        public string UsuarioSesion { get; set; } = "";

        private IConexion? iConexion;
        private void AbrirConexion()
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");
        }

        private void RegistrarAuditoria(string descripcion, string accion)
        {
            iConexion!.Auditorias!.Add(new Auditorias
            {
                Descripcion = descripcion,
                FechaHora = DateTime.Now,
                Usuario = UsuarioSesion, 
                Accion = accion
            });
            iConexion.SaveChanges();
        }

        public List<Roles> Consultar()
        {
            AbrirConexion();
            var lista = iConexion!.Roles!.ToList();
            RegistrarAuditoria("Se realizó una consulta en Roles", "Consulta");
            return lista;
        }

        public Roles Guardar(Roles entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (NombreExiste(entidad.Nombre!))
            {
                throw new Exception("Ya existe un rol con el nombre '" + entidad.Nombre + "'");
            }

            iConexion!.Roles!.Add(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se guardó el rol '" + entidad.Nombre + "'",
                "Guardado");

            return entidad;
        }

        public Roles Eliminar(Roles entidad)
        {
            AbrirConexion();

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El rol con ID " + entidad.Id + " no existe en el sistema");
            }

            int usuariosConRol = iConexion!.Usuarios!
                .Count(u => u.Rol!= null && u.Rol.Id == entidad.Id);

            if (usuariosConRol > 0)
            {
                throw new Exception(
                    "No se puede eliminar el rol '" + entidad.Nombre + "' — " +
                    "tiene " + usuariosConRol + " usuario(s) asignado(s). " +
                    "Reasigne los usuarios antes de eliminar el rol");
            }

            int permisosDelRol = iConexion.Permisos!
                .Count(p => p.Rol!= null && p.Rol.Id == entidad.Id);

            if (permisosDelRol > 0)
            {
                throw new Exception(
                    "No se puede eliminar el rol '" + entidad.Nombre + "' — " +
                    "tiene " + permisosDelRol + " permiso(s) asociado(s). " +
                    "Elimine los permisos antes de eliminar el rol");
            }

            iConexion.Roles!.Remove(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se eliminó el rol '" + entidad.Nombre + "' con ID " + entidad.Id,
                "Eliminacion");

            return entidad;
        }

        public Roles Modificar(Roles entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El rol con ID " + entidad.Id + " no existe en el sistema");
            }

            bool nombreDuplicado = iConexion!.Roles!.Any(r =>
                r.Nombre == entidad.Nombre && r.Id != entidad.Id);

            if (nombreDuplicado)
            {
                throw new Exception("Ya existe otro rol con el nombre '" + entidad.Nombre + "'");
            }

            iConexion.Roles!.Update(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se modificó el rol con ID " + entidad.Id,
                "Modificacion");

            return entidad;
        }

        public bool ValidarId(int id)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.Roles!.Any(r => r.Id == id);
        }

        public bool NombreExiste(string nombre)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.Roles!.Any(r => r.Nombre == nombre);
        }

        public Roles ConsultarPorId(int id)
        {
            AbrirConexion();

            var rol = iConexion!.Roles!.FirstOrDefault(r => r.Id == id);
            if (rol == null)
            {
                throw new Exception("No se encontró ningún rol con ID " + id);
            }

            RegistrarAuditoria(
                "Se consultó el rol con ID: " + id,
                "Consulta por ID");

            return rol;
        }

        public void ValidarDatos(Roles entidad)
        {
            if (entidad == null)
                throw new Exception("La información del rol es obligatoria");

            if (string.IsNullOrEmpty(entidad.Nombre))
                throw new Exception("El nombre del rol es obligatorio");

            if (entidad.Nombre.Length < 3)
                throw new Exception("El nombre del rol debe tener al menos 3 caracteres");
        }
    }
}