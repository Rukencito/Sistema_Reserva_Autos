using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ResenasController : ControllerBase
    {
        private IReseñasNegocio? IReseñasNegocio;

        public ResenasController()
        {
            this.IReseñasNegocio = new ReseñasNegocio();
        }

        [HttpGet]
        public List<Resenas> Consultar()
        {
            if (this.IReseñasNegocio == null)
                throw new Exception("No implementado");
            return this.IReseñasNegocio!.Consultar();
        }

        [HttpPost]
        public Resenas Guardar(Resenas entidad)
        {
            if (this.IReseñasNegocio == null)
                throw new Exception("No implementado");
            return this.IReseñasNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Resenas Modificar(Resenas id)
        {
            if (this.IReseñasNegocio == null)
                throw new Exception("No implementado");
            return this.IReseñasNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Resenas Eliminar(Resenas id)
        {
            if (this.IReseñasNegocio == null)
                throw new Exception("No implementado");
            return this.IReseñasNegocio!.Eliminar(id);
        }
    }
}
