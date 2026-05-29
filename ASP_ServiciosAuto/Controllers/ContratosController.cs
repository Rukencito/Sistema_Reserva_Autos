using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ContratosController : ControllerBase
    {
        private IContratosNegocio? IContratosNegocio;

        public ContratosController()
        {
            this.IContratosNegocio = new ContratosNegocio();
        }

        private void AsignarUsuarioSesion()
        {
            string? usuario = HttpContext.Request.Headers["X-Usuario"].FirstOrDefault();

            ((ContratosNegocio)IContratosNegocio!).UsuarioSesion =
                string.IsNullOrEmpty(usuario) ? "Desconocido" : usuario;
        }

        [HttpGet]
        public List<Contratos> Consultar()
        {
            AsignarUsuarioSesion();

            if (this.IContratosNegocio == null)
                throw new Exception("No implementado");
            return this.IContratosNegocio!.Consultar();
        }

        [HttpPost]
        public Contratos Guardar(Contratos entidad)
        {
            AsignarUsuarioSesion();
            if (this.IContratosNegocio == null)
                throw new Exception("No implementado");
            return this.IContratosNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Contratos Modificar(Contratos entidad)
        {
            AsignarUsuarioSesion();
            if (this.IContratosNegocio == null)
                throw new Exception("No implementado");
            return this.IContratosNegocio!.Modificar(entidad);
        }

        [HttpDelete]

        public Contratos Eliminar(Contratos entidad)
        {
            AsignarUsuarioSesion();
            if (this.IContratosNegocio == null)
                throw new Exception("No implementado");
            return this.IContratosNegocio!.Eliminar(entidad);
        }

        [HttpGet]

        public Contratos ConsultarPorId(int id)
        {
            AsignarUsuarioSesion();
            if (this.IContratosNegocio == null)
                throw new Exception("No implementado");
            return this.IContratosNegocio!.ConsultarPorId(id);
        }

        [HttpGet]
        public List<Contratos> ConsultarPorAlquiler(int alquilerId)
        {
            AsignarUsuarioSesion();
            if (this.IContratosNegocio == null)
                throw new Exception("No implementado");
            return this.IContratosNegocio!.ConsultarPorAlquiler(alquilerId);
        }
    }
}
