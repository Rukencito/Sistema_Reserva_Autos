using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.modelo;

namespace Lib_Unitarias_Autos
{
    [TestClass]
    public class DuenosUT
    {
        private DuenosNegocio? negocio;
        private Duenos? entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Consultar();
            ValidarId();
            ValidarCedula();
            ConsultarPorCedula();
            VerificarEstadoDueno();
            ConsultarActivos();
            AgregarAuto();
            QuitarAuto();
            Modificar();
        }

        private void Consultar()
        {
            negocio = new DuenosNegocio();

            var lista = negocio.Consultar();

            if (lista.Count > 0)
                return;

            throw new Exception("No hay dueños registrados");
        }

        private void ValidarId()
        {
            negocio = new DuenosNegocio();

            bool existe = negocio.ValidarId(1);

            if (existe)
                return;

            throw new Exception("El dueño no existe");
        }

        private void ValidarCedula()
        {
            negocio = new DuenosNegocio();
            bool existe = negocio.ValidarCedula("11223344");

            if (existe)
                return;

            throw new Exception("La cédula no existe");
        }

        private void ConsultarPorCedula()
        {
            negocio = new DuenosNegocio();

            entidad = negocio.ConsultarPorCedula("11223344");

            if (entidad != null)
                return;

            throw new Exception("No se encontró el dueño");
        }

        private void VerificarEstadoDueno()
        {
            negocio = new DuenosNegocio();

            bool estado = negocio.VerificarEstadoDueno(1);

            if (estado)
                return;

            throw new Exception("El dueño no está activo");
        }

        private void ConsultarActivos()
        {
            negocio = new DuenosNegocio();

            var lista = negocio.ConsultarActivos();

            if (lista.Count >= 0)
                return;

            throw new Exception("Error consultando dueños activos");
        }

        private void AgregarAuto()
        {
            negocio = new DuenosNegocio();

            entidad = negocio.AgregarAuto(1);

            if (entidad.CantidadAutos > 0)
                return;

            throw new Exception("No se agregó el auto");
        }

        private void QuitarAuto()
        {
            negocio = new DuenosNegocio();
            entidad = negocio.QuitarAuto(1);

            if (entidad.CantidadAutos >= 0)
                return;

            throw new Exception("No se quitó el auto");
        }

        private void Modificar()
        {
            negocio = new DuenosNegocio();

            entidad = negocio.ConsultarPorCedula("11223344");

            entidad.Telefono = "3008888888";

            var resultado = negocio.Modificar(entidad);

            if (resultado.Telefono == "3008888888")
                return;

            throw new Exception("No se modificó el dueño");
        }
    }
}