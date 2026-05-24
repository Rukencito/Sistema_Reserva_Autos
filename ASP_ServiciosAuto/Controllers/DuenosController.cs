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

        [HttpGet]
        public List<Duenos> Consultar()
        {
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.Consultar();
        }

        [HttpPost]
        public Duenos Guardar(Duenos entidad)
        {
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.Guardar(entidad);
        }
        [HttpPut]
        public Duenos Modificar(Duenos entidad)
        {
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.Modificar(entidad);
        }

        [HttpDelete]

        public Duenos Eliminar(Duenos entidad)
        {
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.Eliminar(entidad);
        }

        [HttpGet]
        public bool ValidarId(int id)
        {
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.ValidarId(id);
        }

        [HttpGet]
        public bool ValidarCedula(string cedula)
        {
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.ValidarCedula(cedula);
        }

        [HttpGet]
        public Duenos ConsultarPorCedula(string cedula)
        {
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.ConsultarPorCedula(cedula);
        }

        [HttpGet]
        public bool VerificarEstadoDueno(int duenoId)
        {
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.VerificarEstadoDueno(duenoId);
        }

        [HttpPut]
        public Duenos AgregarAuto(int duenoId)
        {
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.AgregarAuto(duenoId);
        }

        [HttpPut]
        public Duenos QuitarAuto(int duenoId)
        {
            if (this.IDuenosNegocio == null)
                throw new Exception("No implementado");
            return this.IDuenosNegocio!.QuitarAuto(duenoId);

        }
    }
}
