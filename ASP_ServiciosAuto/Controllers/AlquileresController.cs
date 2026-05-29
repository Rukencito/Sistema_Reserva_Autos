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

        private void AsignarUsuarioSesion()
        {
            string? usuario = HttpContext.Request.Headers["X-Usuario"].FirstOrDefault();

            ((AlquileresNegocio)IAlquileresNegocio!).UsuarioSesion =
                string.IsNullOrEmpty(usuario) ? "Desconocido" : usuario;
        }


        [HttpGet]
        public List<Alquileres> Consultar()
        {
            AsignarUsuarioSesion();
            if (this.IAlquileresNegocio == null)
                throw new Exception("No implementado");
            return this.IAlquileresNegocio!.Consultar();
        }

        [HttpPost]
        public Alquileres Guardar(Alquileres entidad)
        {
            AsignarUsuarioSesion();
            if (this.IAlquileresNegocio == null)
                throw new Exception("No implementado");
            return this.IAlquileresNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Alquileres Modificar(Alquileres entidad)
        {
            AsignarUsuarioSesion();
            if (this.IAlquileresNegocio == null)
                throw new Exception("No implementado");
            return this.IAlquileresNegocio!.Modificar(entidad);
        }

        [HttpDelete]

        public Alquileres Eliminar(Alquileres entidad)
        {
            AsignarUsuarioSesion();
            if (this.IAlquileresNegocio == null)
                throw new Exception("No implementado");
            return this.IAlquileresNegocio!.Eliminar(entidad);
        }

        [HttpGet]
        public List<Alquileres> ConsultarEstadoAlquiler(bool estadoAlquiler)
        {
            AsignarUsuarioSesion();
            if (this.IAlquileresNegocio == null)
                throw new Exception("No implementado");
            return this.IAlquileresNegocio.ConsultarEstadoAlquiler(estadoAlquiler);
        }

        [HttpGet]
        public List<Alquileres> ConsultarAlquileresPorCliente(int clienteId)
        {
            AsignarUsuarioSesion();
            if (this.IAlquileresNegocio == null)
                throw new Exception("No implementado");
            return this.IAlquileresNegocio.ConsultarAlquileresPorCliente(clienteId);
        }

        [HttpGet]
        public bool ExisteCruceDeFechas(int autoId, DateTime fechaInicio, DateTime fechaFin)
        {
            AsignarUsuarioSesion();
            if (this.IAlquileresNegocio == null)
                throw new Exception("No implementado");
            return this.IAlquileresNegocio.ExisteCruceDeFechas(autoId, fechaInicio, fechaFin);
        }

        [HttpGet]
        public decimal CalcularTotalPrecio(decimal precioAlquiler, DateTime fechaInicio, DateTime fechaFin)
        {
            AsignarUsuarioSesion();
            if (this.IAlquileresNegocio == null)
                throw new Exception("No implementado");
            return this.IAlquileresNegocio.CalcularTotalPrecio(precioAlquiler, fechaInicio, fechaFin);
        }
    }

}

