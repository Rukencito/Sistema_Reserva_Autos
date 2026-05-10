using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;

namespace Lib_Unitarias_Autos
{
    [TestClass]
    public sealed class ReseñasUT
    {
        [TestMethod]
        public void TestMethod1()
        {

            IConexion conexion = new Conexion();
            conexion.string_conexion = "server=localhost;Integrated Security=True;TrustServerCertificate=true;database=db_SistemaAutos;";
            var lista = conexion.Reseñas!.ToList();

        }

    }
}