using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class InventariosController : ControllerBase
    {
        private IInventariosNegocio? IInventariosNegocio;

        public InventariosController()
        {
            this.IInventariosNegocio = new InventariosNegocio();
        }

        [HttpGet]
        public List<Inventarios> Consultar()
        {
            if (this.IInventariosNegocio == null)
                throw new Exception("No implementado");
            return this.IInventariosNegocio!.Consultar();
        }

        [HttpPost]
        public Inventarios Guardar(Inventarios entidad)
        {
            if (this.IInventariosNegocio == null)
                throw new Exception("No implementado");
            return this.IInventariosNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Inventarios Modificar(Inventarios entidad)
        {
            if (this.IInventariosNegocio == null)
                throw new Exception("No implementado");
            return this.IInventariosNegocio!.Modificar(entidad);
        }

        [HttpDelete]

        public Inventarios Eliminar(Inventarios entidad)
        {
            if (this.IInventariosNegocio == null)
                throw new Exception("No implementado");
            return this.IInventariosNegocio!.Eliminar(entidad);
        }

        [HttpGet]
        public bool ValidarId(int id)
        {
            if (this.IInventariosNegocio == null)
                throw new Exception("No implementado");
            return this.IInventariosNegocio!.ValidarId(id);
        }

        [HttpGet]
        public Inventarios ConsultarPorId(int id)
        {
            if (this.IInventariosNegocio == null)
                throw new Exception("No implementado");
            return this.IInventariosNegocio!.ConsultarPorId(id);
        }

        [HttpGet]
        public List<Inventarios> ConsultarPorUbicacion(string ubicacion)
        {
            if (this.IInventariosNegocio == null)
                throw new Exception("No implementado");
            return this.IInventariosNegocio!.ConsultarPorUbicacion(ubicacion);
        }

        [HttpPut]
        public Inventarios AgregarStock(int inventarioId, int cantidad, decimal precioUnitario)
        {
            if (this.IInventariosNegocio == null)
                throw new Exception("No implementado");
            return this.IInventariosNegocio!.AgregarStock(inventarioId, cantidad, precioUnitario);
        }

        [HttpPut]
        public Inventarios ReducirStock(int inventarioId, int cantidad, decimal precioUnitario)
        {
            if (this.IInventariosNegocio == null)
                throw new Exception("No implementado");
            return this.IInventariosNegocio!.ReducirStock(inventarioId, cantidad, precioUnitario);
        }

        [HttpPut]
        public Inventarios RecalcularValorTotal(int inventarioId)
        {
            if (this.IInventariosNegocio == null)
                throw new Exception("No implementado");
            return this.IInventariosNegocio!.RecalcularValorTotal(inventarioId);
        }
    }
}
