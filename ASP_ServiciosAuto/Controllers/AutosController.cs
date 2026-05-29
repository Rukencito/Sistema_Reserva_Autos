using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ServiciosAuto.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AutosController : ControllerBase
    {
        private IAutosNegocio? IAutosNegocio;

        public AutosController()
        {
            this.IAutosNegocio = new AutosNegocio();
        }

        private void AsignarUsuarioSesion()
        {
            string? usuario = HttpContext.Request.Headers["X-Usuario"].FirstOrDefault();

            ((AutosNegocio)IAutosNegocio!).UsuarioSesion =
                string.IsNullOrEmpty(usuario) ? "Desconocido" : usuario;
        }

        [HttpGet]
        public List<Autos> Consultar()
        {
            AsignarUsuarioSesion();
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.Consultar();
        }

        [HttpPost]
        public Autos Guardar(Autos entidad)
        {
            AsignarUsuarioSesion();
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Autos Modificar(Autos entidad)
        {
            AsignarUsuarioSesion();
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.Modificar(entidad);
        }

        [HttpDelete]

        public Autos Eliminar(Autos entidad)
        {
            AsignarUsuarioSesion();
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.Eliminar(entidad);
        }

        [HttpGet]
        public Autos ConsultarPorPlaca(string placa)
        {
            AsignarUsuarioSesion();
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.ConsultarPorPlaca(placa);
        }

        [HttpGet]
        public List<Autos> ConsultarPorMarca(string marca)
        {
            AsignarUsuarioSesion();
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.ConsultarPorMarca(marca);
        }

        [HttpGet]
        public List<Autos> ConsultarPorModelo(string modelo)
        {
            AsignarUsuarioSesion();
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.ConsultarPorModelo(modelo);
        }

        [HttpGet]
        public List<Autos> ConsultarDisponibles()
        {
            AsignarUsuarioSesion();
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.ConsultarDisponibles();
        }

        [HttpGet]
        public bool VerificarDisponibilidad(string placa)
        {
            AsignarUsuarioSesion();
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.VerificarDisponibilidad(placa);
        }

        [HttpPut]
        public bool CambiarEstado(string placa, bool nuevoEstado)
        {
            AsignarUsuarioSesion();
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.CambiarEstado(placa, nuevoEstado);
        }
    }
}
