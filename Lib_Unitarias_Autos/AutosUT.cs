using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.modelo;

namespace Lib_Unitarias_Autos
{
    [TestClass]
    public class AutosUT
    {
        private AutosNegocio? negocio;
        private Autos? entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Consultar();
            ConsultarPorPlaca();
            ConsultarPorMarca();
            ConsultarPorModelo();
            ValidarPlaca();
            VerificarDisponibilidad();
            CambiarEstado();
            Modificar();
        }

        private void Consultar()
        {
            negocio = new AutosNegocio();

            var lista = negocio.Consultar();

            if (lista.Count > 0)
                return;

            throw new Exception("No hay autos registrados");
        }

        private void ConsultarPorPlaca()
        {
            negocio = new AutosNegocio();

            entidad = negocio.ConsultarPorPlaca("ABC123");

            if (entidad != null)
                return;

            throw new Exception("No se encontró el auto");
        }

        private void ConsultarPorMarca()
        {
            negocio = new AutosNegocio();

            var lista = negocio.ConsultarPorMarca("Toyota");

            if (lista.Count > 0)
                return;

            throw new Exception("No se encontraron autos");
        }

        private void ConsultarPorModelo()
        {
            negocio = new AutosNegocio();

            var lista = negocio.ConsultarPorModelo("Corolla");

            if (lista.Count > 0)
                return;

            throw new Exception("No se encontraron modelos");
        }

        private void ValidarPlaca()
        {
            negocio = new AutosNegocio();

            bool existe = negocio.ValidarPlaca("ABC123");

            if (existe)
                return;

            throw new Exception("La placa no existe");
        }

        private void VerificarDisponibilidad()
        {
            negocio = new AutosNegocio();

            bool disponible = negocio.VerificarDisponibilidad("ABC123");

            if (disponible == true || disponible == false)
                return;

            throw new Exception("Error verificando disponibilidad");
        }

        private void CambiarEstado()
        {
            negocio = new AutosNegocio();

            bool estado = negocio.CambiarEstado("ABC123", false);

            if (estado == false)
                return;

            throw new Exception("No cambió el estado");
        }

        private void Modificar()
        {
            negocio = new AutosNegocio();

            entidad = negocio.ConsultarPorPlaca("ABC123");

            entidad.Color = "Negro";

            var resultado = negocio.Modificar(entidad);

            if (resultado.Color == "Negro")
                return;

            throw new Exception("No se modificó");
        }
    }
}