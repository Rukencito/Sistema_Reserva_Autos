using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DuenosController : ControllerBase
    {
        private IDuenosNegocio? IDuenosNegocio;

        public DuenosController()
        {
            this.IDuenosNegocio = new DuenosNegocio();
        }

        [HttpGet]
        public List<Duenos> Consultar()
        {
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.Consultar();
        }

        [HttpPost]
        public Duenos Guardar(Duenos entidad)
        {
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Duenos Modificar(Duenos id)
        {
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Duenos Eliminar(Duenos id)
        {
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.Eliminar(id);
        }
    }
}
