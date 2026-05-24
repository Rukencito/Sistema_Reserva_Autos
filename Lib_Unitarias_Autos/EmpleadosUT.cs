using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.modelo;

namespace Lib_Unitarias_Autos
{
    [TestClass]
    public class EmpleadosUT
    {
        private EmpleadosNegocio? negocio;
        private Empleados? entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Consultar();
            ValidarId();
            ValidarCedula();
            ConsultarPorCedula();
            ConsultarPorCargo();
            ConsultarPorSucursal();
            CalcularSalarioTotal();
            Modificar();
        }

        private void Consultar()
        {
            negocio = new EmpleadosNegocio();

            var lista = negocio.Consultar();

            if (lista.Count > 0)
                return;

            throw new Exception("No hay empleados registrados");
        }

        private void ValidarId()
        {
            negocio = new EmpleadosNegocio();

            bool existe = negocio.ValidarId(1);

            if (existe)
                return;

            throw new Exception("El empleado no existe");
        }

        private void ValidarCedula()
        {
            negocio = new EmpleadosNegocio();

            bool existe = negocio.ValidarCedula("87654321");

            if (existe)
                return;

            throw new Exception("La cédula no existe");
        }

        private void ConsultarPorCedula()
        {
            negocio = new EmpleadosNegocio();

            entidad = negocio.ConsultarPorCedula("87654321");

            if (entidad != null)
                return;

            throw new Exception("No se encontró el empleado");
        }

        private void ConsultarPorCargo()
        {
            negocio = new EmpleadosNegocio();

            var lista = negocio.ConsultarPorCargo("Vendedora");

            if (lista.Count > 0)
                return;

            throw new Exception("No se encontraron empleados");
        }

        private void ConsultarPorSucursal()
        {
            negocio = new EmpleadosNegocio();

            var lista = negocio.ConsultarPorSucursal(1);

            if (lista.Count > 0)
                return;

            throw new Exception("No se encontraron empleados");
        }

        private void CalcularSalarioTotal()
        {
            negocio = new EmpleadosNegocio();

            decimal total = negocio.CalcularSalarioTotal(1);

            if (total > 0)
                return;

            throw new Exception("Error calculando salario");
        }

        private void Modificar()
        {
            negocio = new EmpleadosNegocio();

            entidad = negocio.ConsultarPorCedula("87654321");

            entidad.Telefono = "3007777777";

            var resultado = negocio.Modificar(entidad);

            if (resultado.Telefono == "3007777777")
                return;

            throw new Exception("No se modificó el empleado");
        }
    }
}