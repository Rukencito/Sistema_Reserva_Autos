using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuditoriasController : ControllerBase
    {
        private IAuditoriasNegocio? IAuditoriasNegocio;

        public AuditoriasController()
        {
            this.IAuditoriasNegocio = new AuditoriasNegocio();
        }

        private void AsignarUsuarioSesion()
        {
            string? usuario = HttpContext.Request.Headers["X-Usuario"].FirstOrDefault();
            ((AuditoriasNegocio)IAuditoriasNegocio!).UsuarioSesion =
                string.IsNullOrEmpty(usuario) ? "Desconocido" : usuario;
        }

        [HttpGet]
        public List<Auditorias> Consultar()
        {
            AsignarUsuarioSesion();
            if (this.IAuditoriasNegocio == null)
                throw new Exception("No implementado");
            return this.IAuditoriasNegocio!.Consultar();
        }
    }
}