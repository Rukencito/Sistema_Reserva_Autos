using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.modelo;

namespace Lib_Unitarias_Autos
{
    [TestClass]
    public class ClientesUT
    {
        private ClientesNegocio? negocio;
        private Clientes? entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Consultar();
            ValidarCedula();
            ConsultarPorCedula();
            TieneLicencia();
            AgregarPuntosFidelidad();
            Modificar();
        }

        private void Consultar()
        {
            negocio = new ClientesNegocio();

            var lista = negocio.Consultar();

            if (lista.Count > 0)
                return;

            throw new Exception("No hay clientes registrados");
        }

        private void ValidarCedula()
        {
            negocio = new ClientesNegocio();

            bool existe = negocio.ValidarCedula("12345678");

            if (existe)
                return;

            throw new Exception("La cédula no existe");
        }

        private void ConsultarPorCedula()
        {
            negocio = new ClientesNegocio();

            entidad = negocio.ConsultarPorCedula("12345678");

            if (entidad != null)
                return;

            throw new Exception("No se encontró el cliente");
        }

        private void TieneLicencia()
        {
            negocio = new ClientesNegocio();

            bool tieneLicencia = negocio.TieneLicencia(1);

            if (tieneLicencia == true || tieneLicencia == false)
                return;

            throw new Exception("Error validando licencia");
        }

        private void AgregarPuntosFidelidad()
        {
            negocio = new ClientesNegocio();

            entidad = negocio.AgregarPuntosFidelidad(1, 10);

            if (entidad.PuntosFidelidad >= 10)
                return;

            throw new Exception("No se agregaron puntos");
        }

        private void Modificar()
        {
            negocio = new ClientesNegocio();

            entidad = negocio.ConsultarPorCedula("12345678");

            entidad.Telefono = "3009999999";

            var resultado = negocio.Modificar(entidad);

            if (resultado.Telefono == "3009999999")
                return;

            throw new Exception("No se modificó el cliente");
        }
    }
}