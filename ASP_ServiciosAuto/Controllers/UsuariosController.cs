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

        [HttpGet]
        public List<Usuarios> Consultar()
        {
            if (this.IUsuariosNegocio == null)
                throw new Exception("No implementado");
            return this.IUsuariosNegocio!.Consultar();
        }

        [HttpPost]
        public Usuarios Guardar(Usuarios entidad)
        {
            if (this.IUsuariosNegocio == null)
                throw new Exception("No implementado");
            return this.IUsuariosNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Usuarios Modificar(Usuarios id)
        {
            if (this.IUsuariosNegocio == null)
                throw new Exception("No implementado");
            return this.IUsuariosNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Usuarios Eliminar(Usuarios id)
        {
            if (this.IUsuariosNegocio == null)
                throw new Exception("No implementado");
            return this.IUsuariosNegocio!.Eliminar(id);
        }
    }
}
