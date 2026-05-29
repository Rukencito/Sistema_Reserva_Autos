using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GarantiasController : ControllerBase
    {
        private IGarantiasNegocio? IGarantiasNegocio;

        public GarantiasController()
        {
            this.IGarantiasNegocio = new GarantiasNegocio();
        }

        private void AsignarUsuarioSesion()
        {
            string? usuario = HttpContext.Request.Headers["X-Usuario"].FirstOrDefault();

            ((GarantiasNegocio)IGarantiasNegocio!).UsuarioSesion =
                string.IsNullOrEmpty(usuario) ? "Desconocido" : usuario;
        }

        [HttpGet]
        public List<Garantias> Consultar()
        {
            AsignarUsuarioSesion();
            if (this.IGarantiasNegocio == null)
                throw new Exception("No implementado");
            return this.IGarantiasNegocio!.Consultar();
        }

        [HttpPost]
        public Garantias Guardar(Garantias entidad)
        {
            AsignarUsuarioSesion();
            if (this.IGarantiasNegocio == null)
                throw new Exception("No implementado");
            return this.IGarantiasNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Garantias Modificar(Garantias entidad)
        {
            AsignarUsuarioSesion();
            if (this.IGarantiasNegocio == null)
                throw new Exception("No implementado");
            return this.IGarantiasNegocio!.Modificar(entidad);
        }

        [HttpDelete]

        public Garantias Eliminar(Garantias entidad)
        {
            AsignarUsuarioSesion();
            if (this.IGarantiasNegocio == null)
                throw new Exception("No implementado");
            return this.IGarantiasNegocio!.Eliminar(entidad);
        }

        [HttpGet]
        public Garantias ConsultarPorId(int id)
        {
            AsignarUsuarioSesion();
            if (this.IGarantiasNegocio == null)
                throw new Exception("No implementado");
            return this.IGarantiasNegocio!.ConsultarPorId(id);
        }

        [HttpGet]
        public List<Garantias> ConsultarPorAuto(int autoId)
        {
            AsignarUsuarioSesion();
            if (this.IGarantiasNegocio == null)
                throw new Exception("No implementado");
            return this.IGarantiasNegocio!.ConsultarPorAuto(autoId);
        }

        [HttpGet]
        public bool TieneGarantiaVigente(int autoId)
        {
            AsignarUsuarioSesion();
            if (this.IGarantiasNegocio == null)
                throw new Exception("No implementado");
            return this.IGarantiasNegocio!.TieneGarantiaVigente(autoId);
        }

    }
}
