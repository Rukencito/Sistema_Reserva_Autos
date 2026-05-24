using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.modelo;

namespace Lib_Unitarias_Autos
{
    [TestClass]
    public class DetallesFacturaUT
    {
        private DetallesFacturaNegocio? negocio;
        private DetallesFactura? entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Consultar();
            ValidarId();
            ConsultarPorId();
            ConsultarPorFactura();
            CalcularSubtotalPorFactura();
            Modificar();
        }

        private void Consultar()
        {
            negocio = new DetallesFacturaNegocio();

            var lista = negocio.Consultar();

            if (lista.Count > 0)
                entidad = lista.First();
                return;

            throw new Exception("No hay detalles de factura registrados");
        }

        private void ValidarId()
        {
            negocio = new DetallesFacturaNegocio();

            bool existe = negocio.ValidarId(entidad!.Id);

            if (existe)
                return;

            throw new Exception("El detalle no existe");
        }

        private void ConsultarPorId()
        {
            negocio = new DetallesFacturaNegocio();

            entidad = negocio.ConsultarPorId(entidad!.Id);

            if (entidad != null)
                return;

            throw new Exception("No se encontró el detalle");
        }

        private void ConsultarPorFactura()
        {
            negocio = new DetallesFacturaNegocio();

            var lista = negocio.ConsultarPorFactura(entidad!.Id);

            if (lista.Count >= 0)
                return;

            throw new Exception("No se encontraron detalles de factura");
        }

        private void CalcularSubtotalPorFactura()
        {
            negocio = new DetallesFacturaNegocio();

            decimal subtotal = negocio.CalcularSubtotalPorFactura(entidad!.Id);

            if (subtotal > 0)
                return;

            throw new Exception("Error calculando subtotal");
        }

        private void Modificar()
        {
            negocio = new DetallesFacturaNegocio();

            entidad = negocio.ConsultarPorId(entidad!.Id);

            entidad.TipoFactura = "Servicio";

            var resultado = negocio.Modificar(entidad);

            if (resultado.TipoFactura == "Servicio")
                return;

            throw new Exception("No se modificó el detalle");
        }
    }
}