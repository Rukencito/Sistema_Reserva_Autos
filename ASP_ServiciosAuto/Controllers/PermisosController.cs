using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PermisosController : ControllerBase
    {
        private IPermisosNegocio? IPermisosNegocio;

        public PermisosController()
        {
            this.IPermisosNegocio = new PermisosNegocio();
        }

        private void AsignarUsuarioSesion()
        {
            string? usuario = HttpContext.Request.Headers["X-Usuario"].FirstOrDefault();

            ((PermisosNegocio)IPermisosNegocio!).UsuarioSesion =
                string.IsNullOrEmpty(usuario) ? "Desconocido" : usuario;
        }

        [HttpGet]
        public List<Permisos> Consultar()
        {
            AsignarUsuarioSesion();
            if (this.IPermisosNegocio == null)
                throw new Exception("No implementado");
            return this.IPermisosNegocio!.Consultar();
        }

        [HttpPost]
        public Permisos Guardar(Permisos entidad)
        {
            AsignarUsuarioSesion();
            if (this.IPermisosNegocio == null)
                throw new Exception("No implementado");
            return this.IPermisosNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Permisos Modificar(Permisos id)
        {
            AsignarUsuarioSesion();
            if (this.IPermisosNegocio == null)
                throw new Exception("No implementado");
            return this.IPermisosNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Permisos Eliminar(Permisos id)
        {
            AsignarUsuarioSesion(); 
            if (this.IPermisosNegocio == null)
                throw new Exception("No implementado");
            return this.IPermisosNegocio!.Eliminar(id);
        }

        [HttpGet]
        public bool TienePermiso(int usuarioId, string nombrePermiso)
        {
            AsignarUsuarioSesion();
            if (this.IPermisosNegocio == null)
                throw new Exception("No implementado");
            return this.IPermisosNegocio!.TienePermiso(usuarioId, nombrePermiso);
        }
        [HttpGet]
        public bool TienePermisoPorCorreo(string correo, string nombrePermiso)
        {
            AsignarUsuarioSesion();
            if (this.IPermisosNegocio == null)
                throw new Exception("No implementado");
            return this.IPermisosNegocio!.TienePermisoPorCorreo(correo, nombrePermiso);
        }

       public bool PermisoExisteEnRol(string nombrePermiso, int rolId)
        {
            AsignarUsuarioSesion();
            if (this.IPermisosNegocio == null)
                throw new Exception("No implementado");
            return this.IPermisosNegocio!.PermisoExisteEnRol(nombrePermiso, rolId);
        }


    }
}
