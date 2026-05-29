using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TalleresController : ControllerBase
    {
        private ITalleresNegocio? ITalleresNegocio;

        public TalleresController()
        {
            this.ITalleresNegocio = new TalleresNegocio();
        }

        private void AsignarUsuarioSesion()
        {
            string? usuario = HttpContext.Request.Headers["X-Usuario"].FirstOrDefault();

            ((TalleresNegocio)ITalleresNegocio!).UsuarioSesion =
                string.IsNullOrEmpty(usuario) ? "Desconocido" : usuario;
        }

        [HttpGet]
        public List<Talleres> Consultar()
        {
            AsignarUsuarioSesion();
            if (this.ITalleresNegocio == null)
                throw new Exception("No implementado");
            return this.ITalleresNegocio!.Consultar();
        }

        [HttpPost]
        public Talleres Guardar(Talleres entidad)
        {
            AsignarUsuarioSesion();
            if (this.ITalleresNegocio == null)
                throw new Exception("No implementado");
            return this.ITalleresNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Talleres Modificar(Talleres id)
        {
            AsignarUsuarioSesion();
            if (this.ITalleresNegocio == null)
                throw new Exception("No implementado");
            return this.ITalleresNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Talleres Eliminar(Talleres id)
        {
            AsignarUsuarioSesion();
            if (this.ITalleresNegocio == null)
                throw new Exception("No implementado");
            return this.ITalleresNegocio!.Eliminar(id);
        }
    }
}
