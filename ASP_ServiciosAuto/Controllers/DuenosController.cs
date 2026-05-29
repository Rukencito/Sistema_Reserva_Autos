using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DuenosController : ControllerBase
    {
        private IDuenosNegocio? IDuenosNegocio;

        public DuenosController()
        {
            this.IDuenosNegocio = new DuenosNegocio();
        }

        private void AsignarUsuarioSesion()
        {
            string? usuario = HttpContext.Request.Headers["X-Usuario"].FirstOrDefault();

            ((DuenosNegocio)IDuenosNegocio!).UsuarioSesion =
                string.IsNullOrEmpty(usuario) ? "Desconocido" : usuario;
        }

        [HttpGet]
        public List<Duenos> Consultar()
        {
            AsignarUsuarioSesion();
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.Consultar();
        }

        [HttpPost]
        public Duenos Guardar(Duenos entidad)
        {
            AsignarUsuarioSesion();
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Duenos Modificar(Duenos entidad)
        {
            AsignarUsuarioSesion();
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.Modificar(entidad);
        }

        [HttpDelete]

        public Duenos Eliminar(Duenos entidad)
        {
            AsignarUsuarioSesion();
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.Eliminar(entidad);
        }

        [HttpGet]
        public Duenos ConsultarPorCedula(string cedula)
        {
            AsignarUsuarioSesion();
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.ConsultarPorCedula(cedula);
        }

        [HttpGet]
        public bool VerificarEstadoDueno(int duenoId)
        {
            AsignarUsuarioSesion();
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.VerificarEstadoDueno(duenoId);
        }

        [HttpPut]
        public Duenos AgregarAuto(int duenoId)
        {
            AsignarUsuarioSesion();
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.AgregarAuto(duenoId);
        }

        [HttpPut]
        public Duenos QuitarAuto(int duenoId)
        {
            AsignarUsuarioSesion();
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.QuitarAuto(duenoId);

        }
    }
}
