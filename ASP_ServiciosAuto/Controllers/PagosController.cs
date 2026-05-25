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
        public Pagos Modificar(Pagos entidad)
        {
            if (this.IPagosNegocio == null)
                throw new Exception("No implementado");
            return this.IPagosNegocio!.Modificar(entidad);
        }

        [HttpDelete]

        public Pagos Eliminar(Pagos entidad)
        {
            if (this.IPagosNegocio == null)
                throw new Exception("No implementado");
            return this.IPagosNegocio!.Eliminar(entidad);
        }

        [HttpGet]
        public Pagos ConsultarPorId(int id)
        {
            if (this.IPagosNegocio == null)
                throw new Exception("No implementado");
            return this.IPagosNegocio!.ConsultarPorId(id);
        }

        [HttpGet]
        public List<Pagos> ConsultarPorFactura(int facturaId)
        {
            if (this.IPagosNegocio == null)
                throw new Exception("No implementado");
            return this.IPagosNegocio!.ConsultarPorFactura(facturaId);
        }
    }
}
