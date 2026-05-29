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

        private void AsignarUsuarioSesion()
        {
            string? usuario = HttpContext.Request.Headers["X-Usuario"].FirstOrDefault();

            ((VentasNegocio)IVentasNegocio!).UsuarioSesion =
                string.IsNullOrEmpty(usuario) ? "Desconocido" : usuario;
        }

        [HttpGet]
        public List<Ventas> Consultar()
        {
            AsignarUsuarioSesion();
            if (this.IVentasNegocio == null)
                throw new Exception("No implementado");
            return this.IVentasNegocio!.Consultar();
        }

        [HttpPost]
        public Ventas Guardar(Ventas entidad)
        {
            AsignarUsuarioSesion();
            if (this.IVentasNegocio == null)
                throw new Exception("No implementado");
            return this.IVentasNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Ventas Modificar(Ventas id)
        {
            AsignarUsuarioSesion();
            if (this.IVentasNegocio == null)
                throw new Exception("No implementado");
            return this.IVentasNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Ventas Eliminar(Ventas id)
        {
            AsignarUsuarioSesion();
            if (this.IVentasNegocio == null)
                throw new Exception("No implementado");
            return this.IVentasNegocio!.Eliminar(id);
        }

        [HttpGet]
        public List<Ventas> ConsultarPorCliente(int idCliente)
        {
            AsignarUsuarioSesion();
            if (this.IVentasNegocio == null)
                throw new Exception("No implementado");
            return this.IVentasNegocio!.ConsultarPorCliente(idCliente);
        }
    }
}
