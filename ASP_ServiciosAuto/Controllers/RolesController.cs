using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RolesController : ControllerBase
    {
        private IRolesNegocio? IRolesNegocio;

        public RolesController()
        {
            this.IRolesNegocio = new RolesNegocio();
        }

        [HttpGet]
        public List<Roles> Consultar()
        {
            if (this.IRolesNegocio == null)
                throw new Exception("No implementado");
            return this.IRolesNegocio!.Consultar();
        }

        [HttpPost]
        public Roles Guardar(Roles entidad)
        {
            if (this.IRolesNegocio == null)
                throw new Exception("No implementado");
            return this.IRolesNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Roles Modificar(Roles id)
        {
            if (this.IRolesNegocio == null)
                throw new Exception("No implementado");
            return this.IRolesNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Roles Eliminar(Roles id)
        {
            if (this.IRolesNegocio == null)
                throw new Exception("No implementado");
            return this.IRolesNegocio!.Eliminar(id);
        }

        [HttpGet]
        public bool NombreExiste(string nombre)
        {
            if (this.IRolesNegocio == null)
                throw new Exception("No implementado");
            return this.IRolesNegocio!.NombreExiste(nombre);
        }

        [HttpGet]
        public Roles ConsultarPorId(int id)
        {
            if (this.IRolesNegocio == null)
                throw new Exception("No implementado");
            return this.IRolesNegocio!.ConsultarPorId(id);
        }
    }
}
