using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;

namespace Lib_Unitarias_Autos
{
    [TestClass]
    public sealed class PromocionesUT
    {
        private PromocionesNegocio? negocio;
        private Promociones? entidad;

        [TestMethod]
        public void TestMethod1()
        {
            Consultar();
            ValidarId();
            Modificar();

        }
        private void Consultar()
        {
            negocio = new PromocionesNegocio();
            var lista = negocio.Consultar();

            if (lista.Count > 0)
            {
                entidad = lista.First();
                return;
            }

            throw new Exception("Error consultando promociones: lista vacía");
        }
        private void ValidarId()
        {
            negocio = new PromocionesNegocio();

            bool existe = negocio.ValidarId(entidad!.Id);

            if (existe)
                return;

            throw new Exception("El ID de la promoción no es válido");
        }

        private void Modificar()
        {
            negocio = new PromocionesNegocio();

            entidad!.Descripcion = "Promoción modificada test";
            entidad.Descuento = 25;

            var resultado = negocio.Modificar(entidad);

            if (resultado.Descripcion == "Promoción modificada test" &&
                resultado.Descuento == 25)
                return;

            throw new Exception("No se modificó la promoción");
        }

    }
}