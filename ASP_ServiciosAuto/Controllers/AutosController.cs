using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AutosController : ControllerBase
    {
        private IAutosNegocio? IAutosNegocio;

        public AutosController()
        {
            this.IAutosNegocio = new AutosNegocio();
        }

        [HttpGet]
        public List<Autos> Consultar()
        {
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.Consultar();
        }

        [HttpPost]
        public Autos Guardar(Autos entidad)
        {
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Autos Modificar(Autos id)
        {
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Autos Eliminar(Autos id)
        {
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.Eliminar(id);
        }
    }
}
