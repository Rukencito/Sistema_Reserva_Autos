using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ParqueaderosController : ControllerBase
    {
        private IParqueaderosNegocio? IParqueaderosNegocio;

        public ParqueaderosController()
        {
            this.IParqueaderosNegocio = new ParqueaderosNegocio();
        }

        [HttpGet]
        public List<Parqueaderos> Consultar()
        {
            if (this.IParqueaderosNegocio == null)
                throw new Exception("No implementado");
            return this.IParqueaderosNegocio!.Consultar();
        }

        [HttpPost]
        public Parqueaderos Guardar(Parqueaderos entidad)
        {
            if (this.IParqueaderosNegocio == null)
                throw new Exception("No implementado");
            return this.IParqueaderosNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Parqueaderos Modificar(Parqueaderos id)
        {
            if (this.IParqueaderosNegocio == null)
                throw new Exception("No implementado");
            return this.IParqueaderosNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Parqueaderos Eliminar(Parqueaderos id)
        {
            if (this.IParqueaderosNegocio == null)
                throw new Exception("No implementado");
            return this.IParqueaderosNegocio!.Eliminar(id);
        }
    }
}
