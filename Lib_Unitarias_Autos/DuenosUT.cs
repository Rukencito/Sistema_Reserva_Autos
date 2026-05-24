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
            AgregarAuto();
            QuitarAuto();
            Modificar();
        }

        private void Consultar()
        {
            negocio = new DuenosNegocio();

            var lista = negocio.Consultar();

            if (lista.Count > 0)
                entidad = lista.First();
            return;

            throw new Exception("No hay dueños registrados");
        }

        private void ValidarId()
        {
            negocio = new DuenosNegocio();

            bool existe = negocio.ValidarId(entidad!.Id);

            if (existe)
                return;

            throw new Exception("El dueño no existe");
        }

        private void ValidarCedula()
        {
            negocio = new DuenosNegocio();
            bool existe = negocio.ValidarCedula(entidad!.Cedula!);

            if (existe)
                return;

            throw new Exception("La cédula no existe");
        }

        private void ConsultarPorCedula()
        {
            negocio = new DuenosNegocio();

            entidad = negocio.ConsultarPorCedula(entidad!.Cedula!);

            if (entidad != null)
                return;

            throw new Exception("No se encontró el dueño");
        }
        private void VerificarEstadoDueno()
        {
            negocio = new DuenosNegocio();

            bool estado = negocio.VerificarEstadoDueno(entidad!.Id);

            if (estado)
                return;

            throw new Exception("El dueño no está activo");
        }

        private void AgregarAuto()
        {
            negocio = new DuenosNegocio();

            entidad = negocio.AgregarAuto(entidad!.Id);

            if (entidad.CantidadAutos > 0)
                return;

            throw new Exception("No se agregó el auto");
        }

        private void QuitarAuto()
        {
            negocio = new DuenosNegocio();
            entidad = negocio.QuitarAuto(entidad!.Id);

            if (entidad.CantidadAutos >= 0)
                return;

            throw new Exception("No se quitó el auto");
        }

        private void Modificar()
        {
            negocio = new DuenosNegocio();

            entidad = negocio.ConsultarPorCedula(entidad!.Cedula!);

            entidad.Telefono = "3008888888";

            var resultado = negocio.Modificar(entidad);

            if (resultado.Telefono == "3008888888")
                return;

            throw new Exception("No se modificó el dueño");
        }
    }
}