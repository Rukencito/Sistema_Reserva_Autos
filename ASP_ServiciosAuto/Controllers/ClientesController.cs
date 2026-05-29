using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ClientesController : ControllerBase
    {
        private IClientesNegocio? IClientesNegocio;

        public ClientesController()
        {
            this.IClientesNegocio = new ClientesNegocio();
        }

        private void AsignarUsuarioSesion()
        {
            string? usuario = HttpContext.Request.Headers["X-Usuario"].FirstOrDefault();

            ((ClientesNegocio)IClientesNegocio!).UsuarioSesion =
                string.IsNullOrEmpty(usuario) ? "Desconocido" : usuario;
        }

        [HttpGet]
        public List<Clientes> Consultar()
        {
            AsignarUsuarioSesion();
            if (this.IClientesNegocio == null)
                throw new Exception("No implementado");
            return this.IClientesNegocio!.Consultar();
        }

        [HttpPost]
        public Clientes Guardar(Clientes entidad)
        {
            AsignarUsuarioSesion();
            if (this.IClientesNegocio == null)
                throw new Exception("No implementado");
            return this.IClientesNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Clientes Modificar(Clientes entidad)
        {
            AsignarUsuarioSesion();
            if (this.IClientesNegocio == null)
                throw new Exception("No implementado");
            return this.IClientesNegocio!.Modificar(entidad);
        }

        [HttpDelete]

        public Clientes Eliminar(Clientes entidad)
        {
            AsignarUsuarioSesion();
            if (this.IClientesNegocio == null)
                throw new Exception("No implementado");
            return this.IClientesNegocio!.Eliminar(entidad);
        }

        [HttpGet]
        public Clientes ConsultarPorCedula(string cedula)
        {
            AsignarUsuarioSesion();
            if (this.IClientesNegocio == null)
                throw new Exception("No implementado");
            return this.IClientesNegocio!.ConsultarPorCedula(cedula);
        }

        [HttpPut]
        public Clientes AgregarPuntosFidelidad(int clienteId, int puntos)
        {
            AsignarUsuarioSesion();
            if (this.IClientesNegocio == null)
                throw new Exception("No implementado");
            return this.IClientesNegocio!.AgregarPuntosFidelidad(clienteId, puntos);
        }

        [HttpGet]
        public bool TieneLicencia(int clienteId)
        {
            AsignarUsuarioSesion();
            if (this.IClientesNegocio == null)
                throw new Exception("No implementado");
            return this.IClientesNegocio!.TieneLicencia(clienteId);
        }
    }
}
