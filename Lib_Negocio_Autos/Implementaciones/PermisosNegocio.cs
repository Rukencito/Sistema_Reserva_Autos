using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class PermisosNegocio : IPermisosNegocio
    {
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
                Usuario = "UsuarioActual", // Reemplaza con el usuario de sesión
                Accion = accion
            });
            iConexion.SaveChanges();
        }

        public List<Permisos> Consultar()
        {
            AbrirConexion();
            var lista = iConexion!.Permisos!.ToList();
            RegistrarAuditoria("Se realizó una consulta en Permisos", "Consulta");
            return lista;
        }

        public Permisos Guardar(Permisos entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (PermisoExisteEnRol(entidad.Nombre!, entidad.Rol!.Id))
                throw new Exception(
                    "El rol ya tiene un permiso llamado '" + entidad.Nombre + "'");

            iConexion!.Permisos!.Add(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se guardó el permiso '" + entidad.Nombre + "' para el rol ID " + entidad.Rol!.Id,
                "Guardado");

            return entidad;
        }

        public Permisos Eliminar(Permisos entidad)
        {
            AbrirConexion();

            if (!ValidarId(entidad.Id))
                throw new Exception($"El permiso con ID {entidad.Id} no existe en el sistema");

            iConexion!.Permisos!.Remove(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se eliminó el permiso '" + entidad.Nombre + "' con ID " + entidad.Id,
                "Eliminacion");

            return entidad;
        }

        public Permisos Modificar(Permisos entidad)
        {
            AbrirConexion();

            ValidarDatos(entidad);

            if (!ValidarId(entidad.Id))
                throw new Exception("El permiso con ID " + entidad.Id + " no existe en el sistema");

            bool nombreDuplicado = iConexion!.Permisos!.Any(p =>
                p.Nombre == entidad.Nombre &&
                p.Rol!= null &&
                p.Rol.Id == entidad.Rol!.Id &&
                p.Id != entidad.Id);

            if (nombreDuplicado)
                throw new Exception("El rol ya tiene otro permiso llamado '" + entidad.Nombre + "'");

            iConexion.Permisos!.Update(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se modificó el permiso con ID " + entidad.Id,
                "Modificacion");

            return entidad;
        }
        public bool TienePermiso(int usuarioId, string nombrePermiso)
        {
            AbrirConexion();

            // Obtener el usuario
            var usuario = iConexion!.Usuarios!.FirstOrDefault(u => u.Id == usuarioId);
            if (usuario == null)
            {
                throw new Exception("No se encontró ningún usuario con ID " + usuarioId);
            }

            // Paso 2: Verificar que tenga rol asignado
            if (usuario.Roles == null)
            {
                RegistrarAuditoria(
                    "Acceso denegado — usuario ID " + usuarioId + " sin rol asignado",
                    "Verificacion Permiso");
                return false;
            }

            // Paso 3: Verificar que el rol esté activo
            if (usuario.Rol!.Estado == false)
            {
                RegistrarAuditoria(
                    "Acceso denegado — rol '" + usuario.Rol.Nombre + "' inactivo",
                    "Verificacion Permiso");
                return false;
            }

            // Paso 4: Buscar el permiso en los permisos del rol
            bool tienePermiso = iConexion.Permisos!.Any(p =>
                p.Rol!= null &&
                p.Rol.Id == usuario.Rol!.Id &&
                p.Nombre!.ToLower() == nombrePermiso.ToLower());

            RegistrarAuditoria(
                "Verificación de permiso '" + nombrePermiso + "' para usuario ID " + usuarioId + " : " +
                $"{(tienePermiso ? "CONCEDIDO" : "DENEGADO")}",
                "Verificacion Permiso");

            return tienePermiso;
        }
        public bool TienePermisoPorCorreo(string correo, string nombrePermiso)
        {
            AbrirConexion();

            var usuario = iConexion!.Usuarios!.FirstOrDefault(u => u.Correo == correo);
            if (usuario == null)
                throw new Exception("No se encontró ningún usuario con el correo " + correo);

            return TienePermiso(usuario.Id, nombrePermiso);
        }
        public bool PermisoExisteEnRol(string nombrePermiso, int rolId)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.Permisos!.Any(p =>
                p.Nombre!.ToLower() == nombrePermiso.ToLower() &&
                p.Rol!= null &&
                p.Rol.Id == rolId);
        }
        public void ValidarDatos(Permisos entidad)
        {
            if (entidad == null)
                throw new Exception("La información del permiso es obligatoria");

            if (string.IsNullOrEmpty(entidad.Nombre))
                throw new Exception("El nombre del permiso es obligatorio");

            if (entidad.Nombre.Length < 3)
                throw new Exception("El nombre del permiso debe tener al menos 3 caracteres");

            if (entidad.Rol == null)
                throw new Exception("El permiso debe estar asociado a un rol");
        }
        public bool ValidarId(int id)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.Permisos!.Any(p => p.Id == id);
        }
    }
}