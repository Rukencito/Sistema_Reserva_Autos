using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages
{
    public class CompletarPerfilModel : PageModel
    {
        private IClientesPresentacion? iClientes;
        private IUsuariosPresentacion? iUsuarios;

        [BindProperty] public string? Nombre { get; set; }
        [BindProperty] public string? Apellido { get; set; }
        [BindProperty] public string? Cedula { get; set; }
        [BindProperty] public int Edad { get; set; }
        [BindProperty] public string? Correo { get; set; }
        [BindProperty] public string? Telefono { get; set; }
        [BindProperty] public bool LicenciaConduccion { get; set; }

        [BindProperty] public int UsuarioId { get; set; }

        public string? MensajeError { get; set; }

        public CompletarPerfilModel()
        {
            iClientes = new ClientesPresentacion();
            iUsuarios = new UsuariosPresentacion();
        }

        public IActionResult OnGet()
        {
            if (TempData["NuevoUsuarioId"] == null)
            {
                return RedirectToPage("/Login");
            }

            UsuarioId = (int)TempData["NuevoUsuarioId"]!;

            Nombre = TempData["NombreUsuario"]?.ToString();
            Apellido = TempData["ApellidoUsuario"]?.ToString();
            Correo = TempData["CorreoUsuario"]?.ToString();
            Telefono = TempData["TelefonoUsuario"]?.ToString();

            return Page();
        }

        public IActionResult OnPostBtGuardar()
        {
            try
            {
                if (string.IsNullOrEmpty(Cedula) || string.IsNullOrEmpty(Nombre))
                {
                    MensajeError = "Nombre y Cédula son obligatorios.";
                    return Page();
                }

                var nuevoCliente = new Clientes
                {
                    Nombre = Nombre,
                    Apellido = Apellido ?? "",
                    Cedula = Cedula,
                    Edad = Edad,
                    Correo = Correo ?? "",
                    Telefono = Telefono ?? "",
                    LicenciaConduccion = LicenciaConduccion,
                    PuntosFidelidad = 0   
                };

                var clienteGuardado = iClientes!.Guardar(nuevoCliente);

                if (clienteGuardado.Id == 0)
                {
                    MensajeError = "No se pudo guardar el cliente. Intentá de nuevo.";
                    return Page();
                }

                var usuarios = iUsuarios!.Consultar();
                var usuario = usuarios.FirstOrDefault(u => u.Id == UsuarioId);

                if (usuario == null)
                {
                    MensajeError = "No se encontró el usuario. Contactá al administrador.";
                    return Page();
                }

                usuario.Clientes = clienteGuardado.Id;
                iUsuarios.Modificar(usuario);

                TempData["RegistroCompleto"] = "¡Perfil completado! Ya podés iniciar sesión.";
                return RedirectToPage("/Login");
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                return Page();
            }
        }
    }
}