using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReservasController : ControllerBase
    {
        private IReservasNegocio? IReservasNegocio;

        public ReservasController()
        {
            this.IReservasNegocio = new ReservasNegocio();
        }

        [HttpGet]
        public List<Reservas> Consultar()
        {
            if (this.IReservasNegocio == null)
                throw new Exception("No implementado");
            return this.IReservasNegocio!.Consultar();
        }

        [HttpPost]
        public Reservas Guardar(Reservas entidad)
        {
            if (this.IReservasNegocio == null)
                throw new Exception("No implementado");
            return this.IReservasNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Reservas Modificar(Reservas id)
        {
            if (this.IReservasNegocio == null)
                throw new Exception("No implementado");
            return this.IReservasNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Reservas Eliminar(Reservas id)
        {
            if (this.IReservasNegocio == null)
                throw new Exception("No implementado");
            return this.IReservasNegocio!.Eliminar(id);
        }
    }
}
