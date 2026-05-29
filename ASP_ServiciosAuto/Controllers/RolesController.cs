using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RolesController : ControllerBase
    {
        private IRolesNegocio? IRolesNegocio;

        public RolesController()
        {
            this.IRolesNegocio = new RolesNegocio();
        }

        private void AsignarUsuarioSesion()
        {
            string? usuario = HttpContext.Request.Headers["X-Usuario"].FirstOrDefault();

            ((RolesNegocio)IRolesNegocio!).UsuarioSesion =
                string.IsNullOrEmpty(usuario) ? "Desconocido" : usuario;
        }

        [HttpGet]
        public List<Roles> Consultar()
        {
            AsignarUsuarioSesion();
            if (this.IRolesNegocio == null)
                throw new Exception("No implementado");
            return this.IRolesNegocio!.Consultar();
        }

        [HttpPost]
        public Roles Guardar(Roles entidad)
        {
            AsignarUsuarioSesion();
            if (this.IRolesNegocio == null)
                throw new Exception("No implementado");
            return this.IRolesNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Roles Modificar(Roles id)
        {
            AsignarUsuarioSesion();
            if (this.IRolesNegocio == null)
                throw new Exception("No implementado");
            return this.IRolesNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Roles Eliminar(Roles id)
        {
            AsignarUsuarioSesion();
            if (this.IRolesNegocio == null)
                throw new Exception("No implementado");
            return this.IRolesNegocio!.Eliminar(id);
        }

        [HttpGet]
        public bool NombreExiste(string nombre)
        {
            AsignarUsuarioSesion();
            if (this.IRolesNegocio == null)
                throw new Exception("No implementado");
            return this.IRolesNegocio!.NombreExiste(nombre);
        }

        [HttpGet]
        public Roles ConsultarPorId(int id)
        {
            AsignarUsuarioSesion();
            if (this.IRolesNegocio == null)
                throw new Exception("No implementado");
            return this.IRolesNegocio!.ConsultarPorId(id);
        }
    }
}
