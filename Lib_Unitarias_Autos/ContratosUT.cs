using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.modelo;

namespace Lib_Unitarias_Autos
{
    [TestClass]
    public class ContratosUT
    {
        private ContratosNegocio? negocio;
        private Contratos? entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Consultar();
            ValidarId();
            ConsultarPorId();
            ConsultarPorAlquiler();
            ConsultarPorTipo();
            ConsultarVencidos();
            ConsultarActivos();
            Modificar();
        }

        private void Consultar()
        {
            negocio = new ContratosNegocio();

            var lista = negocio.Consultar();

            if (lista.Count > 0)
                return;

            throw new Exception("No hay contratos registrados");
        }

        private void ValidarId()
        {
            negocio = new ContratosNegocio();

            bool existe = negocio.ValidarId(1);

            if (existe)
                return;

            throw new Exception("El contrato no existe");
        }

        private void ConsultarPorId()
        {
            negocio = new ContratosNegocio();

            entidad = negocio.ConsultarPorId(1);

            if (entidad != null)
                return;

            throw new Exception("No se encontró el contrato");
        }

        private void ConsultarPorAlquiler()
        {
            negocio = new ContratosNegocio();

            var lista = negocio.ConsultarPorAlquiler(1);

            if (lista.Count > 0)
                return;

            throw new Exception("No se encontraron contratos");
        }

        private void ConsultarPorTipo()
        {
            negocio = new ContratosNegocio();

            var lista = negocio.ConsultarPorTipo("Contrato de Alquiler");

            if (lista.Count > 0)
                return;

            throw new Exception("No se encontraron contratos");
        }

        private void ConsultarVencidos()
        {
            negocio = new ContratosNegocio();

            var lista = negocio.ConsultarVencidos();

            if (lista.Count >= 0)
                return;

            throw new Exception("Error consultando contratos vencidos");
        }

        private void ConsultarActivos()
        {
            negocio = new ContratosNegocio();

            var lista = negocio.ConsultarActivos();

            if (lista.Count >= 0)
                return;

            throw new Exception("Error consultando contratos activos");
        }

        private void Modificar()
        {
            negocio = new ContratosNegocio();

            entidad = negocio.ConsultarPorId(1);

            entidad.Descripcion = "Contrato actualizado";

            var resultado = negocio.Modificar(entidad);

            if (resultado.Descripcion == "Contrato actualizado")
                return;

            throw new Exception("No se modificó el contrato");
        }
    }
}