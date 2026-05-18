using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DevolucionesController : ControllerBase
    {
        private IDevolucionesNegocio? IDevolucionesNegocio;

        public DevolucionesController()
        {
            this.IDevolucionesNegocio = new DevolucionesNegocio();
        }

        [HttpGet]
        public List<Devoluciones> Consultar()
        {
            if (this.IDevolucionesNegocio == null)
                throw new Exception("No implementado");
            return this.IDevolucionesNegocio!.Consultar();
        }

        [HttpPost]
        public Devoluciones Guardar(Devoluciones entidad)
        {
            if (this.IDevolucionesNegocio == null)
                throw new Exception("No implementado");
            return this.IDevolucionesNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Devoluciones Modificar(Devoluciones id)
        {
            if (this.IDevolucionesNegocio == null)
                throw new Exception("No implementado");
            return this.IDevolucionesNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Devoluciones Eliminar(Devoluciones id)
        {
            if (this.IDevolucionesNegocio == null)
                throw new Exception("No implementado");
            return this.IDevolucionesNegocio!.Eliminar(id);
        }
    }
}
