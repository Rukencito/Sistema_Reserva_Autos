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

        private void AsignarUsuarioSesion()
        {
            string? usuario = HttpContext.Request.Headers["X-Usuario"].FirstOrDefault();

            ((EmpleadosNegocio)IEmpleadosNegocio!).UsuarioSesion =
                string.IsNullOrEmpty(usuario) ? "Desconocido" : usuario;
        }

        [HttpGet]
        public List<Empleados> Consultar()
        {
            AsignarUsuarioSesion();
            if (this.IEmpleadosNegocio == null)
                throw new Exception("No implementado");
            return this.IEmpleadosNegocio!.Consultar();
        }

        [HttpPost]
        public Empleados Guardar(Empleados entidad)
        {
            AsignarUsuarioSesion();
            if (this.IEmpleadosNegocio == null)
                throw new Exception("No implementado");
            return this.IEmpleadosNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Empleados Modificar(Empleados entidad)
        {
            AsignarUsuarioSesion();
            if (this.IEmpleadosNegocio == null)
                throw new Exception("No implementado");
            return this.IEmpleadosNegocio!.Modificar(entidad);
        }

        [HttpDelete]

        public Empleados Eliminar(Empleados entidad)
        {
            AsignarUsuarioSesion();
            if (this.IEmpleadosNegocio == null)
                throw new Exception("No implementado");
            return this.IEmpleadosNegocio!.Eliminar(entidad);
        }

        [HttpGet]
        public Empleados ConsultarPorCedula(string cedula)
        {
            AsignarUsuarioSesion();
            if (this.IEmpleadosNegocio == null)
                throw new Exception("No implementado");
            return this.IEmpleadosNegocio!.ConsultarPorCedula(cedula);
        }

        [HttpGet]
        public List<Empleados> ConsultarPorCargo(string cargo)
        {
            AsignarUsuarioSesion();
            if (this.IEmpleadosNegocio == null)
                throw new Exception("No implementado");
            return this.IEmpleadosNegocio!.ConsultarPorCargo(cargo);
        }

        [HttpGet]
        public List<Empleados> ConsultarPorSucursal(int sucursalId)
        {
            AsignarUsuarioSesion();
            if (this.IEmpleadosNegocio == null)
                throw new Exception("No implementado");
            return this.IEmpleadosNegocio!.ConsultarPorSucursal(sucursalId);
        }

        [HttpGet]
        public decimal CalcularSalarioTotal(int empleadoId)
        {
            AsignarUsuarioSesion();
            if (this.IEmpleadosNegocio == null)
                throw new Exception("No implementado");
            return this.IEmpleadosNegocio!.CalcularSalarioTotal(empleadoId);
        }
    }
}
