
using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages
{
    public class LoginModel : PageModel
    {
        private IUsuariosPresentacion? iUsuarios;

        [BindProperty] public string? Correo { get; set; }
        [BindProperty] public string? Contrasena { get; set; }
        public string? MensajeError { get; set; }

        public LoginModel()
        {
            iUsuarios = new UsuariosPresentacion();
        }

        public void OnGet() { }

        public void OnPostBtLimpiar()
        {
            Correo = string.Empty;
            Contrasena = string.Empty;
        }

        public void OnPostBtEntrar()
        {
            try
            {
                if (string.IsNullOrEmpty(Correo) || string.IsNullOrEmpty(Contrasena))
                {
                    MensajeError = "Por favor ingrese correo y contraseña";
                    return;
                }

                var lista = iUsuarios!.Consultar();
                var usuario = lista.FirstOrDefault(u =>
                    u.Correo == Correo &&
                    u.Contraseña == Contrasena);

                if (usuario == null)
                {
                    MensajeError = "Correo o contraseña incorrectos";
                    return;
                }

                // Guardar sesión
                HttpContext.Session.SetString("Usuario", usuario.Correo!);
                HttpContext.Session.SetString("UsuarioId", usuario.Id.ToString());
                HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre!);
                HttpContext.Session.SetString("RolId", usuario.Roles.ToString()!);

                // Redirigir según rol
                if (usuario.Roles == 1)
                {
                    HttpContext.Response.Redirect("/Ventanas/Admin/IndexAdmin");
                }
                else if (usuario.Roles == 2)
                {
                    HttpContext.Session.SetString("EntidadId", usuario.Clientes.ToString()!);
                    HttpContext.Response.Redirect("/Ventanas/Cliente/IndexClientes");
                }
                else if (usuario.Roles == 3)
                {
                    HttpContext.Session.SetString("EntidadId", usuario.Empleados.ToString()!);
                    HttpContext.Response.Redirect("/Ventanas/Empleado/IndexEmpleados");
                }
                else if (usuario.Roles == 4)
                {
                    HttpContext.Session.SetString("EntidadId", usuario.Duenos.ToString()!);
                    HttpContext.Response.Redirect("/Ventanas/Dueno/IndexDuenos");
                }
                else
                {
                    HttpContext.Response.Redirect("/Index");
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
            }
        }

        public void OnPostBtCerrar()
        {
            HttpContext.Session.Clear();
            HttpContext.Response.Redirect("/Login");
        }
    }
}