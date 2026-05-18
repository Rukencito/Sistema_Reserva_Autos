using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PagosController : ControllerBase
    {
        private IPagosNegocio? IPagosNegocio;

        public PagosController()
        {
            this.IPagosNegocio = new PagosNegocio();
        }

        [HttpGet]
        public List<Pagos> Consultar()
        {
            if (this.IPagosNegocio == null)
                throw new Exception("No implementado");
            return this.IPagosNegocio!.Consultar();
        }

        [HttpPost]
        public Pagos Guardar(Pagos entidad)
        {
            if (this.IPagosNegocio == null)
                throw new Exception("No implementado");
            return this.IPagosNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Pagos Modificar(Pagos id)
        {
            if (this.IPagosNegocio == null)
                throw new Exception("No implementado");
            return this.IPagosNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Pagos Eliminar(Pagos id)
        {
            if (this.IPagosNegocio == null)
                throw new Exception("No implementado");
            return this.IPagosNegocio!.Eliminar(id);
        }
    }
}
