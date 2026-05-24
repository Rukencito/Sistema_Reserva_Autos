using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.modelo;

namespace Lib_Unitarias_Autos
{
    [TestClass]
    public class MantenimientosUT
    {
        private MantenimientosNegocio? negocio;
        private Mantenimientos? entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Consultar();
            ConsultarPorId();
            ConsultarPorAuto();
            ConsultarPorTaller();
            ValidarId();
            FinalizarMantenimiento();
        }

        private void Consultar()
        {
            negocio = new MantenimientosNegocio();

            var lista = negocio.Consultar();

            if (lista.Count > 0)
            {
                entidad = lista.First();
                return;
            }

            throw new Exception("No existen mantenimientos registrados");
        }

        private void ConsultarPorId()
        {
            negocio = new MantenimientosNegocio();

            entidad = negocio.ConsultarPorId(entidad!.Id);

            if (entidad != null)
                return;

            throw new Exception("No se pudo consultar el mantenimiento por ID");
        }

        private void ConsultarPorAuto()
        {
            negocio = new MantenimientosNegocio();

            if (entidad!.Auto == null)
                throw new Exception("El mantenimiento no tiene auto asociado");

            var lista = negocio.ConsultarPorAuto(entidad.Auto!.Id);

            if (lista.Count > 0)
                return;

            throw new Exception("No se encontraron mantenimientos para el auto");
        }

        private void ConsultarPorTaller()
        {
            negocio = new MantenimientosNegocio();

            if (entidad!.Talleres == null)
                throw new Exception("El mantenimiento no tiene taller asociado");

            var lista = negocio.ConsultarPorTaller(entidad.Taller!.Id);

            if (lista.Count > 0)
                return;

            throw new Exception("No se encontraron mantenimientos para el taller");
        }


        private void ValidarId()
        {
            negocio = new MantenimientosNegocio();

            bool existe = negocio.ValidarId(entidad!.Id);

            if (existe)
                return;

            throw new Exception("El ID del mantenimiento no es válido");
        }

        private void FinalizarMantenimiento()
        {
            negocio = new MantenimientosNegocio();

            var mantenimiento = negocio.FinalizarMantenimiento(entidad!.Id);

            if (mantenimiento != null)
                return;

            throw new Exception("No se pudo finalizar el mantenimiento");
        }
    }
}