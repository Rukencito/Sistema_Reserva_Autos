using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;

namespace Lib_Unitarias_Autos
{
    [TestClass]
    public sealed class SucursalesUT
    {
        private SucursalesNegocio? negocio;
        private Sucursales? entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Consultar();
            ValidarId();
            ConsultarPorCiudad();
            Modificar();

        }
        private void Consultar()
        {
            negocio = new SucursalesNegocio();
            var lista = negocio.Consultar();

           
            if (lista.Count > 0)
            {
                entidad = lista.First();
                return;
            }

            throw new Exception("Error consultando sucursales: lista vacía");
        }

        private void ValidarId()
        {
            negocio = new SucursalesNegocio();

            bool existe = negocio.ValidarId(entidad!.Id);

            if (existe)
                return;

            throw new Exception("El ID de la sucursal no es válido");
        }

        private void ConsultarPorCiudad()
        {
            negocio = new SucursalesNegocio();

            var lista = negocio.ConsultarPorCiudad(entidad!.Ciudad!);

            if (lista.Count > 0)
                return;

            throw new Exception($"No se encontraron sucursales en la ciudad: {entidad.Ciudad}");
        }

        private void Modificar()
        {
            negocio = new SucursalesNegocio();
            entidad!.Nombre = "Sucursal Modificada Test";

            var resultado = negocio.Modificar(entidad);

            if (resultado.Nombre == "Sucursal Modificada Test")
                return;

            throw new Exception("No se modificó la sucursal");
        }
    }
    }