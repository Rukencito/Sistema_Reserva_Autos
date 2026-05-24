using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.modelo;

namespace Lib_Unitarias_Autos
{
    [TestClass]
    public class DevolucionesUT
    {
        private DevolucionesNegocio? negocio;
        private Devoluciones? entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Consultar();
            ValidarId();
            ExisteDevolucionParaAlquiler();
            ConsultarPorId();
            ConsultarPorAlquiler();
            Modificar();
        }

        private void Consultar()
        {
            negocio = new DevolucionesNegocio();

            var lista = negocio.Consultar();

            if (lista.Count > 0)
                return;

            throw new Exception("No hay devoluciones registradas");
        }

        private void ValidarId()
        {
            negocio = new DevolucionesNegocio();

            bool existe = negocio.ValidarId(1);

            if (existe)
                return;

            throw new Exception("La devolución no existe");
        }

        private void ExisteDevolucionParaAlquiler()
        {
            negocio = new DevolucionesNegocio();

            bool existe = negocio.ExisteDevolucionParaAlquiler(1);

            if (existe == true || existe == false)
                return;

            throw new Exception("Error validando devolución");
        }

        private void ConsultarPorId()
        {
            negocio = new DevolucionesNegocio();

            entidad = negocio.ConsultarPorId(1);

            if (entidad != null)
                return;

            throw new Exception("No se encontró la devolución");
        }

        private void ConsultarPorAlquiler()
        {
            negocio = new DevolucionesNegocio();

            entidad = negocio.ConsultarPorAlquiler(1);

            if (entidad != null)
                return;

            throw new Exception("No se encontró devolución para el alquiler");
        }

        private void Modificar()
        {
            negocio = new DevolucionesNegocio();

            entidad = negocio.ConsultarPorId(1);

            entidad.NivelCombustible = 80;

            var resultado = negocio.Modificar(entidad);

            if (resultado.NivelCombustible == 80)
                return;

            throw new Exception("No se modificó la devolución");
        }
    }
}