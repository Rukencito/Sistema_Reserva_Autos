using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SegurosController : ControllerBase
    {
        private ISegurosNegocio? ISegurosNegocio;

        public SegurosController()
        {
            this.ISegurosNegocio = new SegurosNegocio();
        }

        private void AsignarUsuarioSesion()
        {
            string? usuario = HttpContext.Request.Headers["X-Usuario"].FirstOrDefault();

            ((SegurosNegocio)ISegurosNegocio!).UsuarioSesion =
                string.IsNullOrEmpty(usuario) ? "Desconocido" : usuario;
        }

        [HttpGet]
        public List<Seguros> Consultar()
        {
            AsignarUsuarioSesion();
            if (this.ISegurosNegocio == null)
                throw new Exception("No implementado");
            return this.ISegurosNegocio!.Consultar();
        }

        [HttpPost]
        public Seguros Guardar(Seguros entidad)
        {
            AsignarUsuarioSesion();
            if (this.ISegurosNegocio == null)
                throw new Exception("No implementado");
            return this.ISegurosNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Seguros Modificar(Seguros id)
        {
            AsignarUsuarioSesion();
            if (this.ISegurosNegocio == null)
                throw new Exception("No implementado");
            return this.ISegurosNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Seguros Eliminar(Seguros id)
        {
            AsignarUsuarioSesion();
            if (this.ISegurosNegocio == null)
                throw new Exception("No implementado");
            return this.ISegurosNegocio!.Eliminar(id);
        }

        [HttpGet]
        public Seguros ConsultarPorId(int id)
        {
            AsignarUsuarioSesion();
            if (this.ISegurosNegocio == null)
                throw new Exception("No implementado");
            return this.ISegurosNegocio!.ConsultarPorId(id);
        }
    }
}
