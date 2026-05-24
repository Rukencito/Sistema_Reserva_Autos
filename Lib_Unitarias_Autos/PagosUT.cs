using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.modelo;

namespace Lib_Unitarias_Autos
{
    [TestClass]
    public class PagosUT
    {
        private PagosNegocio? negocio;
        private Pagos? entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Consultar();
            ConsultarPorId();
            ConsultarPorFactura();
            ValidarId();
        }

        private void Consultar()
        {
            negocio = new PagosNegocio();

            var lista = negocio.Consultar();

            if (lista.Count > 0)
            {
                entidad = lista.First();
                return;
            }

            throw new Exception("No existen pagos registrados");
        }

        private void ConsultarPorId()
        {
            negocio = new PagosNegocio();

            entidad = negocio.ConsultarPorId(entidad!.Id);

            if (entidad != null)
                return;

            throw new Exception("No se pudo consultar el pago por ID");
        }

        private void ConsultarPorFactura()
        {
            negocio = new PagosNegocio();

            if (entidad!.Facturas == null)
            {
                throw new Exception("El pago no tiene factura asociada");
            }

            var lista = negocio.ConsultarPorFactura(entidad.Factura!.Id);

            if (lista.Count > 0)
                return;

            throw new Exception("No se encontraron pagos para la factura");
        }

        private void ValidarId()
        {
            negocio = new PagosNegocio();

            bool existe = negocio.ValidarId(entidad!.Id);

            if (existe)
                return;

            throw new Exception("El ID del pago no es válido");
        }
    }
}