using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SucursalesController : ControllerBase
    {
        private ISucursalesNegocio? ISucursalesNegocio;

        public SucursalesController()
        {
            this.ISucursalesNegocio = new SucursalesNegocio();
        }

        [HttpGet]
        public List<Sucursales> Consultar()
        {
            if (this.ISucursalesNegocio == null)
                throw new Exception("No implementado");
            return this.ISucursalesNegocio!.Consultar();
        }

        [HttpPost]
        public Sucursales Guardar(Sucursales entidad)
        {
            if (this.ISucursalesNegocio == null)
                throw new Exception("No implementado");
            return this.ISucursalesNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Sucursales Modificar(Sucursales id)
        {
            if (this.ISucursalesNegocio == null)
                throw new Exception("No implementado");
            return this.ISucursalesNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Sucursales Eliminar(Sucursales id)
        {
            if (this.ISucursalesNegocio == null)
                throw new Exception("No implementado");
            return this.ISucursalesNegocio!.Eliminar(id);
        }
    }
}
