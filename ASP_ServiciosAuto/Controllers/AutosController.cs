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

        [HttpGet]
        public List<Autos> Consultar()
        {
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.Consultar();
        }

        [HttpPost]
        public Autos Guardar(Autos entidad)
        {
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Autos Modificar(Autos id)
        {
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.Modificar(id);
        }

        [HttpDelete]

        public Autos Eliminar(Autos id)
        {
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.Eliminar(id);
        }

        [HttpGet]
        public bool ValidarPlaca(string placa)
        {
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.ValidarPlaca(placa);
        }

        [HttpGet]
        public Autos ConsultarPorPlaca(string placa)
        {
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.ConsultarPorPlaca(placa);
        }

        [HttpGet]
        public List<Autos> ConsultarPorMarca(string marca)
        {
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.ConsultarPorMarca(marca);
        }

        [HttpGet]
        public List<Autos> ConsultarPorModelo(string modelo)
        {
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.ConsultarPorModelo(modelo);
        }

        [HttpGet]
        public List<Autos> ConsultarDisponibles()
        {
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.ConsultarDisponibles();
        }

        [HttpGet]
        public bool VerificarDisponibilidad(string placa)
        {
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.VerificarDisponibilidad(placa);
        }

        [HttpPut]
        public bool CambiarEstado(string placa, bool nuevoEstado)
        {
            if (this.IAutosNegocio == null)
                throw new Exception("No implementado");
            return this.IAutosNegocio!.CambiarEstado(placa, nuevoEstado);
        }
    }
}
