
using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.modelo;

namespace Lib_Unitarias_Autos
{
    [TestClass]
    public class InventariosUT
    {
        private InventariosNegocio? negocio;
        private Inventarios? entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Consultar();
            ValidarId();
            ConsultarPorId();
            ConsultarPorUbicacion();
            AgregarStock();
            ReducirStock();
            RecalcularValorTotal();
            Modificar();
        }

        private void Consultar()
        {
            negocio = new InventariosNegocio();

            var lista = negocio.Consultar();

            if (lista.Count > 0)
                entidad = lista.First();
                return;

            throw new Exception("No hay inventarios registrados");
        }

        private void ValidarId()
        {
            negocio = new InventariosNegocio();
            bool existe = negocio.ValidarId(entidad!.Id);

            if (existe)
                return;

            throw new Exception("El inventario no existe");
        }

        private void ConsultarPorId()
        {
            negocio = new InventariosNegocio();
            entidad = negocio.ConsultarPorId(entidad!.Id);

            if (entidad != null)
                return;

            throw new Exception("No se encontró el inventario");
        }

        private void ConsultarPorUbicacion()
        {
            negocio = new InventariosNegocio();
            var lista = negocio.ConsultarPorUbicacion("Bodega Principal");

            if (lista.Count >= 0)
                return;

            throw new Exception("No se encontraron inventarios");
        }

        private void AgregarStock()
        {
            negocio = new InventariosNegocio();
            entidad = negocio.AgregarStock(1, 2, 1000000);

            if (entidad.Cantidad > 0)
                return;

            throw new Exception("No se agregó stock");
        }

        private void ReducirStock()
        {
            negocio = new InventariosNegocio();
            entidad = negocio.ReducirStock(1, 1, 1000000);

            if (entidad.Cantidad >= 0)
                return;

            throw new Exception("No se redujo stock");
        }

        private void RecalcularValorTotal()
        {
            negocio = new InventariosNegocio();
            entidad = negocio.RecalcularValorTotal(1);

            if (entidad.Cantidad >= 0)
                return;

            throw new Exception("Error recalculando inventario");
        }

        private void Modificar()
        {
            negocio = new InventariosNegocio();
            entidad = negocio.ConsultarPorId(entidad!.Id);

            entidad.Ubicacion = "Sucursal Principal";

            var resultado = negocio.Modificar(entidad);

            if (resultado.Ubicacion == "Sucursal Principal")
                return;

            throw new Exception("No se modificó el inventario");
        }
    }
}