using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsuariosController : ControllerBase
    {
        private IUsuariosNegocio? IUsuariosNegocio;

        public UsuariosController()
        {
            this.IUsuariosNegocio = new UsuariosNegocio();
        }

        private void AsignarUsuarioSesion()
        {
            string? usuario = HttpContext.Request.Headers["X-Usuario"].FirstOrDefault();

            ((UsuariosNegocio)IUsuariosNegocio!).UsuarioSesion =
                string.IsNullOrEmpty(usuario) ? "Desconocido" : usuario;
        }

        [HttpGet]
        public List<Usuarios> Consultar()
        {
            AsignarUsuarioSesion();
            if (this.IUsuariosNegocio == null)
                throw new Exception("No implementado");
            return this.IUsuariosNegocio!.Consultar();
        }

        [HttpPost]
        public Usuarios Guardar(Usuarios entidad)
        {
            AsignarUsuarioSesion();
            if (this.IUsuariosNegocio == null)
                throw new Exception("No implementado");
            return this.IUsuariosNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Usuarios Modificar(Usuarios id)
        {
            AsignarUsuarioSesion();
            if (this.IUsuariosNegocio == null)
                throw new Exception("No implementado");
            return this.IUsuariosNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Usuarios Eliminar(Usuarios id)
        {
            AsignarUsuarioSesion();
            if (this.IUsuariosNegocio == null)
                throw new Exception("No implementado");
            return this.IUsuariosNegocio!.Eliminar(id);
        }
        [HttpGet]
        public Usuarios ConsultarPorCorreo(string correo)
        {
            AsignarUsuarioSesion();
            if (this.IUsuariosNegocio == null)
                throw new Exception("No implementado");
            return this.IUsuariosNegocio!.ConsultarPorCorreo(correo);
        }

        [HttpPost]
        public Usuarios AsignarRol(int usuarioId, int rolId)
        {
            AsignarUsuarioSesion();
            if (this.IUsuariosNegocio == null)
                throw new Exception("No implementado");
            return this.IUsuariosNegocio!.AsignarRol(usuarioId, rolId);
        }

        [HttpGet]
        public List<Usuarios> ConsultarPorRol(int rolId)
        {
            AsignarUsuarioSesion();
            if (this.IUsuariosNegocio == null)
                throw new Exception("No implementado");
            return this.IUsuariosNegocio!.ConsultarPorRol(rolId);
        }

    }
}
