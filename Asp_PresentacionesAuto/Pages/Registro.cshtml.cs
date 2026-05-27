
using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages
{
    public class RegistroModel : PageModel
    {
        private IUsuariosPresentacion? iUsuarios;

        [BindProperty] public string? Nombre { get; set; }
        [BindProperty] public string? Apellido { get; set; }
        [BindProperty] public string? Telefono { get; set; }
        [BindProperty] public string? Correo { get; set; }
        [BindProperty] public string? Contrasena { get; set; }
        [BindProperty] public string? ConfirmarContrasena { get; set; }

        public string? MensajeError { get; set; }
        public string? MensajeExito { get; set; }

        public RegistroModel()
        {
            iUsuarios = new UsuariosPresentacion();
        }

        public void OnGet() { }

        public void OnPostBtRegistrar()
        {
            try
            {
                if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Correo) ||
                    string.IsNullOrEmpty(Contrasena) || string.IsNullOrEmpty(ConfirmarContrasena))
                {
                    MensajeError = "Todos los campos obligatorios deben completarse.";
                    return;
                }

                if (Contrasena != ConfirmarContrasena)
                {
                    MensajeError = "Las contraseñas no coinciden.";
                    return;
                }

                // Verificar que el correo no exista
                var lista = iUsuarios!.Consultar();
                if (lista.Any(u => u.Correo == Correo))
                {
                    MensajeError = "Ya existe un usuario registrado con ese correo.";
                    return;
                }

                var nuevoUsuario = new Usuarios
                {
                    Nombre = Nombre,
                    Apellido = Apellido ?? "",
                    Telefono = Telefono ?? "",
                    Correo = Correo,
                    Contraseña = Contrasena,
                    Roles = 2  // Cliente por defecto
                };

                iUsuarios.Guardar(nuevoUsuario);

                MensajeExito = "¡Registro exitoso! Ya podés iniciar sesión.";

                // Limpiar campos
                Nombre = Apellido = Telefono = Correo = Contrasena = ConfirmarContrasena = string.Empty;
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
            }
        }
    }
}