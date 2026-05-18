using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class VentasController : ControllerBase
    {
        private IVentasNegocio? IVentasNegocio;

        public VentasController()
        {
            this.IVentasNegocio = new VentasNegocio();
        }

        [HttpGet]
        public List<Ventas> Consultar()
        {
            if (this.IVentasNegocio == null)
                throw new Exception("No implementado");
            return this.IVentasNegocio!.Consultar();
        }

        [HttpPost]
        public Ventas Guardar(Ventas entidad)
        {
            if (this.IVentasNegocio == null)
                throw new Exception("No implementado");
            return this.IVentasNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Ventas Modificar(Ventas id)
        {
            if (this.IVentasNegocio == null)
                throw new Exception("No implementado");
            return this.IVentasNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Ventas Eliminar(Ventas id)
        {
            if (this.IVentasNegocio == null)
                throw new Exception("No implementado");
            return this.IVentasNegocio!.Eliminar(id);
        }
    }
}
