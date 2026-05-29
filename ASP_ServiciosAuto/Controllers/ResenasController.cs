using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ResenasController : ControllerBase
    {
        private IResenasNegocio? IReseñasNegocio;

        public ResenasController()
        {
            this.IReseñasNegocio = new ResenasNegocio();
        }

        private void AsignarUsuarioSesion()
        {
            string? usuario = HttpContext.Request.Headers["X-Usuario"].FirstOrDefault();

            ((ResenasNegocio)IReseñasNegocio!).UsuarioSesion =
                string.IsNullOrEmpty(usuario) ? "Desconocido" : usuario;
        }

        [HttpGet]
        public List<Resenas> Consultar()
        {
            AsignarUsuarioSesion();
            if (this.IReseñasNegocio == null)
                throw new Exception("No implementado");
            return this.IReseñasNegocio!.Consultar();
        }

        [HttpPost]
        public Resenas Guardar(Resenas entidad)
        {
            AsignarUsuarioSesion();
            if (this.IReseñasNegocio == null)
                throw new Exception("No implementado");
            return this.IReseñasNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Resenas Modificar(Resenas id)
        {
            AsignarUsuarioSesion();
            if (this.IReseñasNegocio == null)
                throw new Exception("No implementado");
            return this.IReseñasNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Resenas Eliminar(Resenas id)
        {
            AsignarUsuarioSesion();
            if (this.IReseñasNegocio == null)
                throw new Exception("No implementado");
            return this.IReseñasNegocio!.Eliminar(id);
        }

        [HttpGet]

        public List<Resenas> ConsultarPorCliente(int idCliente)
        {
            AsignarUsuarioSesion();
            if (this.IReseñasNegocio == null)
                throw new Exception("No implementado");
            return this.IReseñasNegocio!.ConsultarPorCliente(idCliente);
        }
    }
}
