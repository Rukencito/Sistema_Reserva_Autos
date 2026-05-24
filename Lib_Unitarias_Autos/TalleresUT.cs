using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;

namespace Lib_Unitarias_Autos
{
    [TestClass]
    public sealed class TalleresUT
    {
        private TalleresNegocio? negocio;
        private Talleres? entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Consultar();
            ValidarId();
            Modificar();


        }

        private void Consultar()
        {
            negocio = new TalleresNegocio();
            var lista = negocio.Consultar();

            if (lista.Count > 0)
            {
                entidad = lista.First();
                return;
            }

            throw new Exception("Error consultando talleres: lista vacía");
        }

        private void ValidarId()
        {
            negocio = new TalleresNegocio();

            bool existe = negocio.ValidarId(entidad!.Id);

            if (existe)
                return;

            throw new Exception("El ID del taller no es válido");
        }
        private void Modificar()
        {
            negocio = new TalleresNegocio();

            entidad!.Nombre = "Taller Modificado Test";
            entidad.Direccion = "Dirección Modificada Test";
            entidad.Telefono = "3001234567";
            entidad.Capacidad = 10;

            var resultado = negocio.Modificar(entidad);

            if (resultado.Nombre == "Taller Modificado Test" &&
                resultado.Direccion == "Dirección Modificada Test" &&
                resultado.Telefono == "3001234567" &&
                resultado.Capacidad == 10)
                return;

            throw new Exception("No se modificó el taller");
        }



    }
}
