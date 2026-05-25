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

        [HttpGet]
        public List<Clientes> Consultar()
        {
            if (this.IClientesNegocio == null)
                throw new Exception("No implementado");
            return this.IClientesNegocio!.Consultar();
        }

        [HttpPost]
        public Clientes Guardar(Clientes entidad)
        {
            if (this.IClientesNegocio == null)
                throw new Exception("No implementado");
            return this.IClientesNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Clientes Modificar(Clientes entidad)
        {
            if (this.IClientesNegocio == null)
                throw new Exception("No implementado");
            return this.IClientesNegocio!.Modificar(entidad);
        }

        [HttpDelete]

        public Clientes Eliminar(Clientes entidad)
        {
            if (this.IClientesNegocio == null)
                throw new Exception("No implementado");
            return this.IClientesNegocio!.Eliminar(entidad);
        }

        [HttpGet]
        public Clientes ConsultarPorCedula(string cedula)
        {
            if (this.IClientesNegocio == null)
                throw new Exception("No implementado");
            return this.IClientesNegocio!.ConsultarPorCedula(cedula);
        }

        [HttpPut]
        public Clientes AgregarPuntosFidelidad(int clienteId, int puntos)
        {
            if (this.IClientesNegocio == null)
                throw new Exception("No implementado");
            return this.IClientesNegocio!.AgregarPuntosFidelidad(clienteId, puntos);
        }

        [HttpGet]
        public bool TieneLicencia(int clienteId)
        {
            if (this.IClientesNegocio == null)
                throw new Exception("No implementado");
            return this.IClientesNegocio!.TieneLicencia(clienteId);
        }
    }
}
