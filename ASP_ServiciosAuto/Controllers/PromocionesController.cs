using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PromocionesController : ControllerBase
    {
        private IPromocionesNegocio? IPromocionesNegocio;

        public PromocionesController()
        {
            this.IPromocionesNegocio = new PromocionesNegocio();
        }

        private void AsignarUsuarioSesion()
        {
            string? usuario = HttpContext.Request.Headers["X-Usuario"].FirstOrDefault();

            ((PromocionesNegocio)IPromocionesNegocio!).UsuarioSesion =
                string.IsNullOrEmpty(usuario) ? "Desconocido" : usuario;
        }

        [HttpGet]
        public List<Promociones> Consultar()
        {
            AsignarUsuarioSesion();
            if (this.IPromocionesNegocio == null)
                throw new Exception("No implementado");
            return this.IPromocionesNegocio!.Consultar();
        }

        [HttpPost]
        public Promociones Guardar(Promociones entidad)
        {
            AsignarUsuarioSesion();
            if (this.IPromocionesNegocio == null)
                throw new Exception("No implementado");
            return this.IPromocionesNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Promociones Modificar(Promociones id)
        {
            AsignarUsuarioSesion();
            if (this.IPromocionesNegocio == null)
                throw new Exception("No implementado");
            return this.IPromocionesNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Promociones Eliminar(Promociones id)
        {
            AsignarUsuarioSesion();
            if (this.IPromocionesNegocio == null)
                throw new Exception("No implementado");
            return this.IPromocionesNegocio!.Eliminar(id);
        }
    }
}
