using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;

namespace Lib_Unitarias_Autos
{
    [TestClass]
    public sealed class VentasUT
    {
        private VentasNegocio? negocio;
        private Ventas? entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Consultar();
            ValidarId();
            Modificar();
        }
        private void Consultar()
        {
            negocio = new VentasNegocio();
            var lista = negocio.Consultar();

            if (lista.Count >= 0)
                entidad = lista.First();
                return;

            throw new Exception("Error consultando ventas");
        }

        private void ValidarId()
        {
            negocio = new VentasNegocio();

            bool existe = negocio.ValidarId(entidad!.Id);

            if (existe)
                return;

            throw new Exception("El ID de la venta no es válido");
        }

        private void Modificar()
        {
            negocio = new VentasNegocio();
            entidad!.TipoPago = "Crédito";
            entidad.PrecioVenta = 48000000;

            var resultado = negocio.Modificar(entidad);

            if (resultado.TipoPago == "Crédito" && resultado.PrecioVenta == 48000000)
                return;

            throw new Exception("No se modificó la venta");
        }

    }
}