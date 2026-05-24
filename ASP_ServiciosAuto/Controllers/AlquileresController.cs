using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AlquileresController : ControllerBase
    {
        private IAlquileresNegocio? IAlquileresNegocio;

        public AlquileresController() {
            this.IAlquileresNegocio = new AlquileresNegocio();
        }

        [HttpGet]
        public List<Alquileres> Consultar()
        {
            if (this.IAlquileresNegocio == null)
                throw new Exception("No implementado");
            return this.IAlquileresNegocio!.Consultar();
        }

        [HttpPost]
        public Alquileres Guardar(Alquileres entidad)
        {
            if (this.IAlquileresNegocio == null)
                throw new Exception("No implementado");
            return this.IAlquileresNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Alquileres Modificar(Alquileres entidad)
        {
            if (this.IAlquileresNegocio == null)
                throw new Exception("No implementado");
            return this.IAlquileresNegocio!.Modificar(entidad);
        }

        [HttpDelete]

        public Alquileres Eliminar(Alquileres entidad)
        {
            if (this.IAlquileresNegocio == null)
                throw new Exception("No implementado");
            return this.IAlquileresNegocio!.Eliminar(entidad);
        }

        [HttpGet]
        public bool ValidarId(int id)
        {
            if (this.IAlquileresNegocio == null)
                throw new Exception("No implementado");
            return this.IAlquileresNegocio.ValidarId(id);
        }

        [HttpGet]
        public List<Alquileres> ConsultarEstadoAlquiler(bool estadoAlquiler)
        {
            if (this.IAlquileresNegocio == null)
                throw new Exception("No implementado");
            return this.IAlquileresNegocio.ConsultarEstadoAlquiler(estadoAlquiler);
        }

        [HttpGet]
        public List<Alquileres> ConsultarAlquileresPorCliente(int clienteId)
        {
            if (this.IAlquileresNegocio == null)
                throw new Exception("No implementado");
            return this.IAlquileresNegocio.ConsultarAlquileresPorCliente(clienteId);
        }

        [HttpGet]
        public bool ExisteCruceDeFechas(int autoId, DateTime fechaInicio, DateTime fechaFin)
        {
            if (this.IAlquileresNegocio == null)
                throw new Exception("No implementado");
            return this.IAlquileresNegocio.ExisteCruceDeFechas(autoId, fechaInicio, fechaFin);
        }

        [HttpGet]
        public decimal CalcularTotalPrecio(decimal precioAlquiler, DateTime fechaInicio, DateTime fechaFin)
        {
            if (this.IAlquileresNegocio == null)
                throw new Exception("No implementado");
            return this.IAlquileresNegocio.CalcularTotalPrecio(precioAlquiler, fechaInicio, fechaFin);
        }
    }

}

