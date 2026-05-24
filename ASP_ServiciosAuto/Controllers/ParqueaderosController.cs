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
        [HttpGet]
        public Parqueaderos ConsultarPorId(int id)
        {
            if (this.IParqueaderosNegocio == null)
                throw new Exception("No implementado");
            return this.IParqueaderosNegocio!.ConsultarPorId(id);
        }

       [HttpGet]
        public int ContarAutosEnParqueadero(int parqueaderoId)
        {
            if (this.IParqueaderosNegocio == null)
                throw new Exception("No implementado");
            return this.IParqueaderosNegocio!.ContarAutosEnParqueadero(parqueaderoId);
        }

        [HttpGet]
        public int ConsultarEspaciosDisponibles(int parqueaderoId)
        {
            if (this.IParqueaderosNegocio == null)
                throw new Exception("No implementado");
            return this.IParqueaderosNegocio!.ConsultarEspaciosDisponibles(parqueaderoId);
        }

        [HttpGet]
        public bool TieneEspacioDisponible(int parqueaderoId)
        {
            if (this.IParqueaderosNegocio == null)
                throw new Exception("No implementado");
            return this.IParqueaderosNegocio!.TieneEspacioDisponible(parqueaderoId);
        }

        [HttpGet]
        public List<Parqueaderos> ConsultarConEspacioDisponible()
        {
            if (this.IParqueaderosNegocio == null)
                throw new Exception("No implementado");
            return this.IParqueaderosNegocio!.ConsultarConEspacioDisponible();
        }

        [HttpGet]
        public List<Autos> ConsultarAutosPorParqueadero(int parqueaderoId)
        {
            if (this.IParqueaderosNegocio == null)
                throw new Exception("No implementado");
            return this.IParqueaderosNegocio!.ConsultarAutosPorParqueadero(parqueaderoId);
        }
    }
}
