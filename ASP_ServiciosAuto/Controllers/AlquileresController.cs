using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AlquileresController : ControllerBase
    {
        private IAlquileresNegocio? IAlquileresNegocio;

        public AlquileresController() {
            this.IAlquileresNegocio = new AlquileresNegocio();
        }

        [HttpGet]
        public List<Alquileres> Consultar()
        {
            if (this.IAlquileresNegocio == null)
                throw new Exception("No implementado");
            return this.IAlquileresNegocio!.Consultar();
        }

        [HttpPost]
        public Alquileres Guardar(Alquileres entidad)
        {
            if (this.IAlquileresNegocio == null)
                throw new Exception("No implementado");
            return this.IAlquileresNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Alquileres Modificar(Alquileres id)
        {
            if (this.IAlquileresNegocio == null)
                throw new Exception("No implementado");
            return this.IAlquileresNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Alquileres Eliminar(Alquileres id)
        {
            if (this.IAlquileresNegocio == null)
                throw new Exception("No implementado");
            return this.IAlquileresNegocio!.Eliminar(id);
        }
    }

}

