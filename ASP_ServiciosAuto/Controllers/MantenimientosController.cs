using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MantenimientosController : ControllerBase
    {
        private IMantenimientosNegocio? IMantenimientosNegocio;

        public MantenimientosController()
        {
            this.IMantenimientosNegocio = new MantenimientosNegocio();
        }

        [HttpGet]
        public List<Mantenimientos> Consultar()
        {
            if (this.IMantenimientosNegocio == null)
                throw new Exception("No implementado");
            return this.IMantenimientosNegocio!.Consultar();
        }

        [HttpPost]
        public Mantenimientos Guardar(Mantenimientos entidad)
        {
            if (this.IMantenimientosNegocio == null)
                throw new Exception("No implementado");
            return this.IMantenimientosNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Mantenimientos Modificar(Mantenimientos id)
        {
            if (this.IMantenimientosNegocio == null)
                throw new Exception("No implementado");
            return this.IMantenimientosNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Mantenimientos Eliminar(Mantenimientos id)
        {
            if (this.IMantenimientosNegocio == null)
                throw new Exception("No implementado");
            return this.IMantenimientosNegocio!.Eliminar(id);
        }
    }
}
