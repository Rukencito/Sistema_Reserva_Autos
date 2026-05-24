using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;

namespace Lib_Unitarias_Autos
{
    [TestClass]
    public sealed class SegurosUT
    {
        private SegurosNegocio? negocio;
        private Seguros? entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Consultar();
            ValidarId();
            Modificar();
         
        }

        private void Consultar()
        {
            negocio = new SegurosNegocio();
            var lista = negocio.Consultar();

            if (lista.Count > 0)
            {
                entidad = lista.First();
                return;
            }

            throw new Exception("Error consultando seguros: lista vacía");
        }

        private void ValidarId()
        {
            negocio = new SegurosNegocio();

            bool existe = negocio.ValidarId(entidad!.Id);

            if (existe)
                return;

            throw new Exception("El ID del seguro no es válido");
        }

        private void Modificar()
        {
            negocio = new SegurosNegocio();

            entidad!.Tipo = "Todo Riesgo";
            entidad.Cobertura = "Cobertura Total";
            entidad.Aseguradora = "Aseguradora Test";

            var resultado = negocio.Modificar(entidad);

            if (resultado.Tipo == "Todo Riesgo" &&
                resultado.Cobertura == "Cobertura Total" &&
                resultado.Aseguradora == "Aseguradora Test")
                return;

            throw new Exception("No se modificó el seguro");
        }

    }
}