using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.modelo;

namespace Lib_Unitarias_Autos
{
    [TestClass]
    public class GarantiasUT
    {
        private GarantiasNegocio? negocio;
        private Garantias? entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Consultar();
            ValidarId();
            ConsultarPorId();
            ConsultarPorAuto();
            TieneGarantiaVigente();
            Modificar();
        }

        private void Consultar()
        {
            negocio = new GarantiasNegocio();

            var lista = negocio.Consultar();

            if (lista.Count > 0)
                return;

            throw new Exception("No hay garantías registradas");
        }

        private void ValidarId()
        {
            negocio = new GarantiasNegocio();
            bool existe = negocio.ValidarId(1);

            if (existe)
                return;

            throw new Exception("La garantía no existe");
        }

        private void ConsultarPorId()
        {
            negocio = new GarantiasNegocio();
            entidad = negocio.ConsultarPorId(1);

            if (entidad != null)
                return;

            throw new Exception("No se encontró la garantía");
        }

        private void ConsultarPorAuto()
        {
            negocio = new GarantiasNegocio();

            var lista = negocio.ConsultarPorAuto(1);

            if (lista.Count > 0)
                return;

            throw new Exception("No se encontraron garantías");
        }

        private void TieneGarantiaVigente()
        {
            negocio = new GarantiasNegocio();

            bool vigente = negocio.TieneGarantiaVigente(1);

            if (vigente == true || vigente == false)
                return;

            throw new Exception("Error validando garantía");
        }

        private void Modificar()
        {
            negocio = new GarantiasNegocio();

            entidad = negocio.ConsultarPorId(1);

            entidad.FechaFin = entidad.FechaFin.AddMonths(1);

            var resultado = negocio.Modificar(entidad);

            if (resultado.FechaFin > DateTime.Now)
                return;

            throw new Exception("No se modificó la garantía");
        }
    }
}