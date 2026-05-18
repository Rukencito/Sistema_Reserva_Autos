using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class InventariosController : ControllerBase
    {
        private IInventariosNegocio? IInventariosNegocio;

        public InventariosController()
        {
            this.IInventariosNegocio = new InventariosNegocio();
        }

        [HttpGet]
        public List<Inventarios> Consultar()
        {
            if (this.IInventariosNegocio == null)
                throw new Exception("No implementado");
            return this.IInventariosNegocio!.Consultar();
        }

        [HttpPost]
        public Inventarios Guardar(Inventarios entidad)
        {
            if (this.IInventariosNegocio == null)
                throw new Exception("No implementado");
            return this.IInventariosNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Inventarios Modificar(Inventarios id)
        {
            if (this.IInventariosNegocio == null)
                throw new Exception("No implementado");
            return this.IInventariosNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Inventarios Eliminar(Inventarios id)
        {
            if (this.IInventariosNegocio == null)
                throw new Exception("No implementado");
            return this.IInventariosNegocio!.Eliminar(id);
        }
    }
}
