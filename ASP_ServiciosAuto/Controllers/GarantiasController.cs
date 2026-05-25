using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GarantiasController : ControllerBase
    {
        private IGarantiasNegocio? IGarantiasNegocio;

        public GarantiasController()
        {
            this.IGarantiasNegocio = new GarantiasNegocio();
        }

        [HttpGet]
        public List<Garantias> Consultar()
        {
            if (this.IGarantiasNegocio == null)
                throw new Exception("No implementado");
            return this.IGarantiasNegocio!.Consultar();
        }

        [HttpPost]
        public Garantias Guardar(Garantias entidad)
        {
            if (this.IGarantiasNegocio == null)
                throw new Exception("No implementado");
            return this.IGarantiasNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Garantias Modificar(Garantias entidad)
        {
            if (this.IGarantiasNegocio == null)
                throw new Exception("No implementado");
            return this.IGarantiasNegocio!.Modificar(entidad);
        }

        [HttpDelete]

        public Garantias Eliminar(Garantias entidad)
        {
            if (this.IGarantiasNegocio == null)
                throw new Exception("No implementado");
            return this.IGarantiasNegocio!.Eliminar(entidad);
        }

        [HttpGet]
        public Garantias ConsultarPorId(int id)
        {
            if (this.IGarantiasNegocio == null)
                throw new Exception("No implementado");
            return this.IGarantiasNegocio!.ConsultarPorId(id);
        }

        [HttpGet]
        public List<Garantias> ConsultarPorAuto(int autoId)
        {
            if (this.IGarantiasNegocio == null)
                throw new Exception("No implementado");
            return this.IGarantiasNegocio!.ConsultarPorAuto(autoId);
        }

        [HttpGet]
        public bool TieneGarantiaVigente(int autoId)
        {
            if (this.IGarantiasNegocio == null)
                throw new Exception("No implementado");
            return this.IGarantiasNegocio!.TieneGarantiaVigente(autoId);
        }

    }
}
