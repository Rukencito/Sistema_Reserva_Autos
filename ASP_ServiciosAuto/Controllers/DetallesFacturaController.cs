using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DetallesFacturaController : ControllerBase
    {
        private IDetallesFacturaNegocio? IDetallesFacturaNegocio;

        public DetallesFacturaController()
        {
            this.IDetallesFacturaNegocio = new DetallesFacturaNegocio();
        }

        [HttpGet]
        public List<DetallesFactura> Consultar()
        {
            if (this.IDetallesFacturaNegocio == null)
                throw new Exception("No implementado");
            return this.IDetallesFacturaNegocio!.Consultar();
        }

        [HttpPost]
        public DetallesFactura Guardar(DetallesFactura entidad)
        {
            if (this.IDetallesFacturaNegocio == null)
                throw new Exception("No implementado");
            return this.IDetallesFacturaNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public DetallesFactura Modificar(DetallesFactura entidad)
        {
            if (this.IDetallesFacturaNegocio == null)
                throw new Exception("No implementado");
            return this.IDetallesFacturaNegocio!.Modificar(entidad);
        }

        [HttpDelete]

        public DetallesFactura Eliminar(DetallesFactura entidad)
        {
            if (this.IDetallesFacturaNegocio == null)
                throw new Exception("No implementado");
            return this.IDetallesFacturaNegocio!.Eliminar(entidad);
        }

        [HttpGet]
        public DetallesFactura ConsultarPorId(int id)
        {
            if (this.IDetallesFacturaNegocio == null)
                throw new Exception("No implementado");
            return this.IDetallesFacturaNegocio!.ConsultarPorId(id);
        }

        [HttpGet]
        public List<DetallesFactura> ConsultarPorFactura(int facturaId)
        {
            if (this.IDetallesFacturaNegocio == null)
                throw new Exception("No implementado");
            return this.IDetallesFacturaNegocio!.ConsultarPorFactura(facturaId);
        }

        [HttpGet]
        public decimal CalcularSubtotalPorFactura(int facturaId)
        {
            if (this.IDetallesFacturaNegocio == null)
                throw new Exception("No implementado");
            return this.IDetallesFacturaNegocio!.CalcularSubtotalPorFactura(facturaId);
        }
    }
}
