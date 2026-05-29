using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DevolucionesController : ControllerBase
    {
        private IDevolucionesNegocio? IDevolucionesNegocio;

        public DevolucionesController()
        {
            this.IDevolucionesNegocio = new DevolucionesNegocio();
        }

        private void AsignarUsuarioSesion()
        {
            string? usuario = HttpContext.Request.Headers["X-Usuario"].FirstOrDefault();

            ((DevolucionesNegocio)IDevolucionesNegocio!).UsuarioSesion =
                string.IsNullOrEmpty(usuario) ? "Desconocido" : usuario;
        }

        [HttpGet]
        public List<Devoluciones> Consultar()
        {
            AsignarUsuarioSesion();
            if (this.IDevolucionesNegocio == null)
                throw new Exception("No implementado");
            return this.IDevolucionesNegocio!.Consultar();
        }

        [HttpPost]
        public Devoluciones Guardar(Devoluciones entidad)
        {
            AsignarUsuarioSesion();
            if (this.IDevolucionesNegocio == null)
                throw new Exception("No implementado");
            return this.IDevolucionesNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Devoluciones Modificar(Devoluciones entidad)
        {
            AsignarUsuarioSesion();
            if (this.IDevolucionesNegocio == null)
                throw new Exception("No implementado");
            return this.IDevolucionesNegocio!.Modificar(entidad);
        }

        [HttpDelete]

        public Devoluciones Eliminar(Devoluciones entidad)
        {
            AsignarUsuarioSesion();
            if (this.IDevolucionesNegocio == null)
                throw new Exception("No implementado");
            return this.IDevolucionesNegocio!.Eliminar(entidad);
        }

        [HttpGet]
        public Devoluciones ConsultarPorAlquiler(int idAlquiler)
        {
            AsignarUsuarioSesion();
            if (this.IDevolucionesNegocio == null)
                throw new Exception("No implementado");
            return this.IDevolucionesNegocio!.ConsultarPorAlquiler(idAlquiler);
        }

        [HttpGet]
        public Devoluciones ConsultarPorId(int id)
        {
            AsignarUsuarioSesion();
            if (this.IDevolucionesNegocio == null)
                throw new Exception("No implementado");
            return this.IDevolucionesNegocio!.ConsultarPorId(id);
        }
    }
}
