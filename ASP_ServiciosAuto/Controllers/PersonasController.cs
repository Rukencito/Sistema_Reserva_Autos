using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PersonasController
    {
        private IPersonasNegocio? IPersonasNegocio;

        public PersonasController()
        {
            this.IPersonasNegocio = new PersonasNegocio();
        }

        [HttpGet]
        public List<Personas> Consultar()
        {
            if (this.IPersonasNegocio == null)
                throw new Exception("No implementado");
            return this.IPersonasNegocio!.Consultar();
        }

        [HttpPost]
        public Personas Guardar(Personas entidad)
        {
            if (this.IPersonasNegocio == null)
                throw new Exception("No implementado");
            return this.IPersonasNegocio!.Guardar(entidad);
        }
    }
}
