using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FacturasController : ControllerBase
    {
        private IFacturasNegocio? IFacturasNegocio;

        public FacturasController()
        {
            this.IFacturasNegocio = new FacturasNegocio();
        }

        [HttpGet]
        public List<Facturas> Consultar()
        {
            if (this.IFacturasNegocio == null)
                throw new Exception("No implementado");
            return this.IFacturasNegocio!.Consultar();
        }

        [HttpPost]
        public Facturas Guardar(Facturas entidad)
        {
            if (this.IFacturasNegocio == null)
                throw new Exception("No implementado");
            return this.IFacturasNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Facturas Modificar(Facturas id)
        {
            if (this.IFacturasNegocio == null)
                throw new Exception("No implementado");
            return this.IFacturasNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Facturas Eliminar(Facturas id)
        {
            if (this.IFacturasNegocio == null)
                throw new Exception("No implementado");
            return this.IFacturasNegocio!.Eliminar(id);
        }
    }
}
