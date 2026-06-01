using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_PresentacionesAuto.Pages.Ventanas.Admin
{
    public class AuditoriasHTMLModel : PageModel
    {
        private IAuditoriasPresentacion? IAuditoriasPresentacion;
        public List<Auditorias>? Lista { get; set; }

        private void IniciarAuditorias()
        {
            var correo = HttpContext.Session.GetString("Usuario") ?? "Sistema";
            IAuditoriasPresentacion = new AuditoriasPresentacion(correo);
        }

        public void OnGet()
        {
            IniciarAuditorias();
            try
            {
                Lista = IAuditoriasPresentacion!.Consultar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtRefrescar()
        {
            IniciarAuditorias();
            try
            {
                Lista = IAuditoriasPresentacion!.Consultar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
    }
}