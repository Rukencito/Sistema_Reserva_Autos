using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MantenimientosController : ControllerBase
    {
        private IMantenimientosNegocio? IMantenimientosNegocio;

        public MantenimientosController()
        {
            this.IMantenimientosNegocio = new MantenimientosNegocio();
        }

        private void AsignarUsuarioSesion()
        {
            string? usuario = HttpContext.Request.Headers["X-Usuario"].FirstOrDefault();

            ((MantenimientosNegocio)IMantenimientosNegocio!).UsuarioSesion =
                string.IsNullOrEmpty(usuario) ? "Desconocido" : usuario;
        }

        [HttpGet]
        public List<Mantenimientos> Consultar()
        {
            AsignarUsuarioSesion();
            if (this.IMantenimientosNegocio == null)
                throw new Exception("No implementado");
            return this.IMantenimientosNegocio!.Consultar();
        }

        [HttpPost]
        public Mantenimientos Guardar(Mantenimientos entidad)
        {
            AsignarUsuarioSesion();
            if (this.IMantenimientosNegocio == null)
                throw new Exception("No implementado");
            return this.IMantenimientosNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Mantenimientos Modificar(Mantenimientos entidad)
        {
            AsignarUsuarioSesion();
            if (this.IMantenimientosNegocio == null)
                throw new Exception("No implementado");
            return this.IMantenimientosNegocio!.Modificar(entidad);
        }

        [HttpDelete]

        public Mantenimientos Eliminar(Mantenimientos entidad)
        {
            AsignarUsuarioSesion();
            if (this.IMantenimientosNegocio == null)
                throw new Exception("No implementado");
            return this.IMantenimientosNegocio!.Eliminar(entidad);
        }

        [HttpGet]
        public Mantenimientos ConsultarPorId(int id)
        {
            AsignarUsuarioSesion();
            if (this.IMantenimientosNegocio == null)
                throw new Exception("No implementado");
            return this.IMantenimientosNegocio!.ConsultarPorId(id);
        }

        [HttpGet]
        public List<Mantenimientos> ConsultarPorAuto(int autoId)
        {
            AsignarUsuarioSesion();
            if (this.IMantenimientosNegocio == null)
                throw new Exception("No implementado");
            return this.IMantenimientosNegocio!.ConsultarPorAuto(autoId);
        }

        [HttpGet]
        public List<Mantenimientos> ConsultarPorTaller(int tallerId)
        {
            AsignarUsuarioSesion();
            if (this.IMantenimientosNegocio == null)
                throw new Exception("No implementado");
            return this.IMantenimientosNegocio!.ConsultarPorTaller(tallerId);
        }

        [HttpPut]
        public Mantenimientos FinalizarMantenimiento(int mantenimientoId)
        {
            AsignarUsuarioSesion();
            if (this.IMantenimientosNegocio == null)
                throw new Exception("No implementado");
            return this.IMantenimientosNegocio!.FinalizarMantenimiento(mantenimientoId);
        }


    }
}
