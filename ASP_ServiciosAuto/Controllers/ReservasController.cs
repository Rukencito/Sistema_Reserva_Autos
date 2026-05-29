using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReservasController : ControllerBase
    {
        private IReservasNegocio? IReservasNegocio;

        public ReservasController()
        {
            this.IReservasNegocio = new ReservasNegocio();
        }
        private void AsignarUsuarioSesion()
        {
            string? usuario = HttpContext.Request.Headers["X-Usuario"].FirstOrDefault();

            ((ReservasNegocio)IReservasNegocio!).UsuarioSesion =
                string.IsNullOrEmpty(usuario) ? "Desconocido" : usuario;
        }

        [HttpGet]
        public List<Reservas> Consultar()
        {
            AsignarUsuarioSesion();
            if (this.IReservasNegocio == null)
                throw new Exception("No implementado");
            return this.IReservasNegocio!.Consultar();
        }

        [HttpPost]
        public Reservas Guardar(Reservas entidad)
        {
            AsignarUsuarioSesion();
            if (this.IReservasNegocio == null)
                throw new Exception("No implementado");
            return this.IReservasNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Reservas Modificar(Reservas id)
        {
            AsignarUsuarioSesion();
            if (this.IReservasNegocio == null)
                throw new Exception("No implementado");
            return this.IReservasNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Reservas Eliminar(Reservas id)
        {
            AsignarUsuarioSesion();
            if (this.IReservasNegocio == null)
                throw new Exception("No implementado");
            return this.IReservasNegocio!.Eliminar(id);
        }
        [HttpGet]

        public bool ValidarReservaDuplicada(int autoId, int clienteId, DateTime fechaVencimiento)
        {
            AsignarUsuarioSesion();
            if (this.IReservasNegocio == null)
                throw new Exception("No implementado");
            return this.IReservasNegocio!.ValidarReservaDuplicada(autoId, clienteId, fechaVencimiento);
        }

        [HttpPut]
        public Reservas CambiarEstado(int reservaId, string nuevoEstado)
        {
            AsignarUsuarioSesion();
            if (this.IReservasNegocio == null)
                throw new Exception("No implementado");
            return this.IReservasNegocio!.CambiarEstado(reservaId, nuevoEstado);
        }
        [HttpGet]
        public List<Reservas> ConsultarPorCliente(int clienteId)
        {
            AsignarUsuarioSesion();
            if (this.IReservasNegocio == null)
                throw new Exception("No implementado");
            return this.IReservasNegocio!.ConsultarPorCliente(clienteId);
        }
    }
}
