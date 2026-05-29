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

        [HttpGet]
        public List<Permisos> Consultar()
        {
            if (this.IPermisosNegocio == null)
                throw new Exception("No implementado");
            return this.IPermisosNegocio!.Consultar();
        }

        [HttpPost]
        public Permisos Guardar(Permisos entidad)
        {
            if (this.IPermisosNegocio == null)
                throw new Exception("No implementado");
            return this.IPermisosNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Permisos Modificar(Permisos id)
        {
            if (this.IPermisosNegocio == null)
                throw new Exception("No implementado");
            return this.IPermisosNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Permisos Eliminar(Permisos id)
        {
            if (this.IPermisosNegocio == null)
                throw new Exception("No implementado");
            return this.IPermisosNegocio!.Eliminar(id);
        }

        [HttpGet]
        public bool TienePermiso(int usuarioId, string nombrePermiso)
        {
            if (this.IPermisosNegocio == null)
                throw new Exception("No implementado");
            return this.IPermisosNegocio!.TienePermiso(usuarioId, nombrePermiso);
        }
        [HttpGet]
        public bool TienePermisoPorCorreo(string correo, string nombrePermiso)
        {
            if (this.IPermisosNegocio == null)
                throw new Exception("No implementado");
            return this.IPermisosNegocio!.TienePermisoPorCorreo(correo, nombrePermiso);
        }

        [HttpGet]
        public bool PermisoExisteEnRol(string nombrePermiso, int rolId)
        {
            if (this.IPermisosNegocio == null)
                throw new Exception("No implementado");
            return this.IPermisosNegocio!.PermisoExisteEnRol(nombrePermiso, rolId);
        }


    }
}
