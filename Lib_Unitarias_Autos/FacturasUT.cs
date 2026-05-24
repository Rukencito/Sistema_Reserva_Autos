using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.modelo;

namespace Lib_Unitarias_Autos
{
    [TestClass]
    public class FacturasUT
    {
        private FacturasNegocio? negocio;
        private Facturas? entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Consultar();
            ValidarId();
            ConsultarPorId();
            ConsultarPorCliente();
            ConsultarPendientes();
            CalcularTotal();
            MarcarComoPagada();
            Modificar();
        }

        private void Consultar()
        {
            negocio = new FacturasNegocio();

            var lista = negocio.Consultar();

            if (lista.Count > 0)
                return;

            throw new Exception("No hay facturas registradas");
        }

        private void ValidarId()
        {
            negocio = new FacturasNegocio();

            bool existe = negocio.ValidarId(1);

            if (existe)
                return;

            throw new Exception("La factura no existe");
        }

        private void ConsultarPorId()
        {
            negocio = new FacturasNegocio();

            entidad = negocio.ConsultarPorId(1);

            if (entidad != null)
                return;

            throw new Exception("No se encontró la factura");
        }

        private void ConsultarPorCliente()
        {
            negocio = new FacturasNegocio();

            var lista = negocio.ConsultarPorCliente(1);

            if (lista.Count > 0)
                return;

            throw new Exception("No se encontraron facturas");
        }

        private void ConsultarPendientes()
        {
            negocio = new FacturasNegocio();

            var lista = negocio.ConsultarPendientes();

            if (lista.Count >= 0)
                return;

            throw new Exception("Error consultando facturas pendientes");
        }

        private void CalcularTotal()
        {
            negocio = new FacturasNegocio();

            entidad = negocio.ConsultarPorId(1);

            decimal total = negocio.CalcularTotal(entidad);

            if (total > 0)
                return;

            throw new Exception("Error calculando total");
        }

        private void MarcarComoPagada()
        {
            negocio = new FacturasNegocio();
            entidad = negocio.MarcarComoPagada(1);

            if (entidad.Estado == true)
                return;

            throw new Exception("No se marcó como pagada");
        }

        private void Modificar()
        {
            negocio = new FacturasNegocio();

            entidad = negocio.ConsultarPorId(1);

            entidad.FechaEmision = DateTime.Now;

            var resultado = negocio.Modificar(entidad);

            if (resultado.FechaEmision.Date == DateTime.Now.Date)
                return;

            throw new Exception("No se modificó la factura");
        }
    }
}