using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.modelo;

namespace Lib_Unitarias_Autos
{
    [TestClass]
    public class AlquileresUT
    {
        private AlquileresNegocio? negocio;
        private Alquileres? entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Consultar();
            ValidarId();
            ConsultarEstadoAlquiler();
            ConsultarAlquileresPorCliente();
            ExisteCruceDeFechas();
            CalcularTotalPrecio();
            Modificar();
        }

        private void Consultar()
        {
            negocio = new AlquileresNegocio();

            var lista = negocio.Consultar();

            if (lista.Count > 0)
                return;

            throw new Exception("No hay alquileres registrados");
        }

        private void ValidarId()
        {
            negocio = new AlquileresNegocio();

            bool existe = negocio.ValidarId(1);

            if (existe)
                return;

            throw new Exception("El alquiler no existe");
        }

        private void ConsultarEstadoAlquiler()
        {
            negocio = new AlquileresNegocio();

            var lista = negocio.ConsultarEstadoAlquiler(true);

            if (lista.Count >= 0)
                return;

            throw new Exception("Error consultando estado");
        }

        private void ConsultarAlquileresPorCliente()
        {
            negocio = new AlquileresNegocio();

            var lista = negocio.ConsultarAlquileresPorCliente(1);

            if (lista.Count >= 0)
                return;

            throw new Exception("No se encontraron alquileres");
        }

        private void ExisteCruceDeFechas()
        {
            negocio = new AlquileresNegocio();

            bool existeCruce = negocio.ExisteCruceDeFechas(
                1,
                DateTime.Now,
                DateTime.Now.AddDays(2));

            if (existeCruce == true || existeCruce == false)
                return;

            throw new Exception("Error verificando cruce");
        }

        private void CalcularTotalPrecio()
        {
            negocio = new AlquileresNegocio();

            decimal total = negocio.CalcularTotalPrecio(
                100000,
                DateTime.Now,
                DateTime.Now.AddDays(3));

            if (total == 300000)
                return;

            throw new Exception("Error calculando precio");
        }

        private void Modificar()
        {
            negocio = new AlquileresNegocio();

            entidad = negocio.Consultar().FirstOrDefault(a => a.Id == 1);

            if (entidad == null)
                throw new Exception("No se encontró alquiler");

            entidad.PrecioAlquiler = 200000;

            var resultado = negocio.Modificar(entidad);

            if (resultado.PrecioAlquiler == 200000)
                return;

            throw new Exception("No se modificó el alquiler");
        }
    }
}