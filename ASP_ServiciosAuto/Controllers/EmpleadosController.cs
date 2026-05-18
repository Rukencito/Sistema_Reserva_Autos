using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmpleadosController : ControllerBase
    {
        private IEmpleadosNegocio? IEmpleadosNegocio;

        public EmpleadosController()
        {
            this.IEmpleadosNegocio = new EmpleadosNegocio();
        }

        [HttpGet]
        public List<Empleados> Consultar()
        {
            if (this.IEmpleadosNegocio == null)
                throw new Exception("No implementado");
            return this.IEmpleadosNegocio!.Consultar();
        }

        [HttpPost]
        public Empleados Guardar(Empleados entidad)
        {
            if (this.IEmpleadosNegocio == null)
                throw new Exception("No implementado");
            return this.IEmpleadosNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Empleados Modificar(Empleados id)
        {
            if (this.IEmpleadosNegocio == null)
                throw new Exception("No implementado");
            return this.IEmpleadosNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Empleados Eliminar(Empleados id)
        {
            if (this.IEmpleadosNegocio == null)
                throw new Exception("No implementado");
            return this.IEmpleadosNegocio!.Eliminar(id);
        }
    }
}
