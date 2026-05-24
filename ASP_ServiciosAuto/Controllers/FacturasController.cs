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
        public Facturas Modificar(Facturas entidad)
        {
            if (this.IFacturasNegocio == null)
                throw new Exception("No implementado");
            return this.IFacturasNegocio!.Modificar(entidad);
        }

        [HttpDelete]

        public Facturas Eliminar(Facturas entidad)
        {
            if (this.IFacturasNegocio == null)
                throw new Exception("No implementado");
            return this.IFacturasNegocio!.Eliminar(entidad);
        }

        [HttpGet]
        public bool ValidarId(int id)
        {
            if (this.IFacturasNegocio == null)
                throw new Exception("No implementado");
            return this.IFacturasNegocio!.ValidarId(id);
        }

        [HttpPut]
        public void CalcularTotal(Facturas id)
        {
            if (this.IFacturasNegocio == null)
                throw new Exception("No implementado");
            this.IFacturasNegocio!.CalcularTotal(id);
        }

        [HttpGet]
        public List<Facturas> ConsultarPorCliente(int id)
        {
            if (this.IFacturasNegocio == null)
                throw new Exception("No implementado");
            return this.IFacturasNegocio!.ConsultarPorCliente(id);
        }

        [HttpGet]
        public Facturas ConsultarPorId(int id)
        {
            if (this.IFacturasNegocio == null)
                throw new Exception("No implementado");
            return this.IFacturasNegocio!.ConsultarPorId(id);
        }

        [HttpGet]
        public List<Facturas> ConsultarPendientes()
        {
            if (this.IFacturasNegocio == null)
                throw new Exception("No implementado");
            return this.IFacturasNegocio!.ConsultarPendientes();
        }

        [HttpPut]
        public void MarcarComoPagada(int id)
        {
            if (this.IFacturasNegocio == null)
                throw new Exception("No implementado");
            this.IFacturasNegocio!.MarcarComoPagada(id);
        }
    }
}
