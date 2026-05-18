using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReseñasController : ControllerBase
    {
        private IReseñasNegocio? IReseñasNegocio;

        public ReseñasController()
        {
            this.IReseñasNegocio = new ReseñasNegocio();
        }

        [HttpGet]
        public List<Reseñas> Consultar()
        {
            if (this.IReseñasNegocio == null)
                throw new Exception("No implementado");
            return this.IReseñasNegocio!.Consultar();
        }

        [HttpPost]
        public Reseñas Guardar(Reseñas entidad)
        {
            if (this.IReseñasNegocio == null)
                throw new Exception("No implementado");
            return this.IReseñasNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Reseñas Modificar(Reseñas id)
        {
            if (this.IReseñasNegocio == null)
                throw new Exception("No implementado");
            return this.IReseñasNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Reseñas Eliminar(Reseñas id)
        {
            if (this.IReseñasNegocio == null)
                throw new Exception("No implementado");
            return this.IReseñasNegocio!.Eliminar(id);
        }
    }
}
