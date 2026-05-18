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

        [HttpGet]
        public List<Seguros> Consultar()
        {
            if (this.ISegurosNegocio == null)
                throw new Exception("No implementado");
            return this.ISegurosNegocio!.Consultar();
        }

        [HttpPost]
        public Seguros Guardar(Seguros entidad)
        {
            if (this.ISegurosNegocio == null)
                throw new Exception("No implementado");
            return this.ISegurosNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Seguros Modificar(Seguros id)
        {
            if (this.ISegurosNegocio == null)
                throw new Exception("No implementado");
            return this.ISegurosNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Seguros Eliminar(Seguros id)
        {
            if (this.ISegurosNegocio == null)
                throw new Exception("No implementado");
            return this.ISegurosNegocio!.Eliminar(id);
        }
    }
}
