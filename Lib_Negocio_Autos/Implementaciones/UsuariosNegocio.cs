using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;
using System.Security.Cryptography;
using System.Text;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class UsuariosNegocio : IUsuariosNegocio
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
                Usuario = "UsuarioActual", // Se reemplaza con el usuario de sesión
                Accion = accion
            });
            iConexion.SaveChanges();
        }

        /// <summary>
        /// Hashea una contraseña usando SHA256.
        /// ✅ CORRECCIÓN BUG #6: las contraseñas nunca se guardan en texto plano.
        /// NOTA: Para producción se recomienda usar BCrypt (NuGet: BCrypt.Net-Next).
        /// </summary>
        private string HashearContrasena(string contrasena)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(contrasena));
            return Convert.ToHexString(bytes);
        }
        public List<Usuarios> Consultar()
        {
            AbrirConexion();
            var lista = iConexion!.Usuarios!.ToList();
            RegistrarAuditoria("Se realizó una consulta en Usuarios", "Consulta");
            return lista;
        }

        public Usuarios Guardar(Usuarios entidad)
        {
            AbrirConexion();

            ValidarDatos(entidad);

            if (CorreoExiste(entidad.Correo!))
            {
                throw new Exception("Ya existe un usuario registrado con el correo " + entidad.Correo);
            }

            entidad.Contraseña = HashearContrasena(entidad.Contraseña!);

            iConexion!.Usuarios!.Add(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se registró el usuario con correo " + entidad.Correo,
                "Guardado");

            return entidad;
        }

        public Usuarios Eliminar(Usuarios entidad)
        {
            AbrirConexion();

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El usuario con ID " + entidad.Id + " no existe en el sistema");
            }

            iConexion!.Usuarios!.Remove(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se eliminó el usuario con ID " + entidad.Id,
                "Eliminacion");

            return entidad;
        }

        public Usuarios Modificar(Usuarios entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El usuario con ID " + entidad.Id + " no existe en el sistema");
            }

            iConexion!.Usuarios!.Update(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se modificó el usuario con ID " + entidad.Id,
                "Modificacion");

            return entidad;
        }
        public bool ValidarId(int id)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.Usuarios!.Any(u => u.Id == id);
        }
        public bool CorreoExiste(string correo)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.Usuarios!.Any(u => u.Correo == correo);
        }
        public Usuarios ConsultarPorCorreo(string correo)
        {
            AbrirConexion();

            var usuario = iConexion!.Usuarios!.FirstOrDefault(u => u.Correo == correo);
            if (usuario == null)
            {
                throw new Exception("No se encontró ningún usuario con el correo " + correo);
            }

            RegistrarAuditoria(
                "Se consultó usuario por correo: " + correo,
                "Consulta por Correo");

            return usuario;
        }

        public Usuarios AsignarRol(int usuarioId, int rolId)
        {
            AbrirConexion();

            var usuario = iConexion!.Usuarios!.FirstOrDefault(u => u.Id == usuarioId);
            if (usuario == null)
            {
                throw new Exception("No se encontró ningún usuario con ID " + usuarioId);
            }

            var rol = iConexion.Roles!.FirstOrDefault(r => r.Id == rolId);
            if (rol == null)
            {
                throw new Exception("No se encontró ningún rol con ID " + rolId);
            }

            if (rol.Estado == false)
            {
                throw new Exception("El rol con ID " + rolId + " está inactivo y no puede asignarse");
            }

            usuario.Rol = rol;
            iConexion.Usuarios!.Update(usuario);

            RegistrarAuditoria(
                "Se asignó el rol '" + rol.Nombre + "' al usuario ID " + usuarioId,
                "Asignacion Rol");

            return usuario;
        }
        public List<Usuarios> ConsultarPorRol(int rolId)
        {
            AbrirConexion();

            var usuarios = iConexion!.Usuarios!
                .Where(u => u.Rol!= null && u.Rol.Id == rolId)
                .ToList();

            RegistrarAuditoria(
                "Se consultaron usuarios del rol ID: " + rolId,
                "Consulta por Rol");

            return usuarios;
        }

        /// <summary>
        /// Método de Login — pendiente de implementar.
        /// Se implementará cuando se configure el sistema de sesiones.
        /// </summary>
        public Usuarios? Login(string correo, string contrasena)
        {
            // TODO: implementar cuando se configure el sistema de sesiones
            // Lógica básica:
            // 1. Buscar usuario por correo
            // 2. Comparar HashearContrasena(contrasena) con usuario.Contraseña
            // 3. Verificar que el rol esté activo
            // 4. Guardar en sesión: HttpContext.Session.SetString("Usuario", usuario.Correo)
            //                       HttpContext.Session.SetString("Rol", usuario._Roles!.Nombre)
            // 5. Registrar en auditoría
            throw new NotImplementedException("Login pendiente de implementar con sesiones");
        }
        public void ValidarDatos(Usuarios entidad)
        {
            if (entidad == null)
                throw new Exception("La información del usuario es obligatoria");

            if (string.IsNullOrEmpty(entidad.Nombre))
                throw new Exception("El nombre del usuario es obligatorio");

            if (string.IsNullOrEmpty(entidad.Apellido))
                throw new Exception("El apellido del usuario es obligatorio");

            if (string.IsNullOrEmpty(entidad.Correo))
                throw new Exception("El correo del usuario es obligatorio");

            if (!entidad.Correo.Contains("@") || !entidad.Correo.Contains("."))
                throw new Exception("El correo del usuario no tiene un formato válido");

            if (string.IsNullOrEmpty(entidad.Contraseña))
                throw new Exception("La contraseña del usuario es obligatoria");

            ValidarContrasena(entidad.Contraseña);
        }

        private void ValidarContrasena(string contrasena)
        {
            if (contrasena.Length < 8)
                throw new Exception("La contraseña debe tener al menos 8 caracteres");

            if (!contrasena.Any(char.IsUpper))
                throw new Exception("La contraseña debe tener al menos una letra mayúscula");

            if (!contrasena.Any(char.IsLower))
                throw new Exception("La contraseña debe tener al menos una letra minúscula");

            if (!contrasena.Any(char.IsDigit))
                throw new Exception("La contraseña debe tener al menos un número");
        }
    }
}