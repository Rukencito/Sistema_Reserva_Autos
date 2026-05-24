using Lib_Negocio_Autos.Implementaciones;
using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;

namespace Lib_Unitarias_Autos
{
   
    [TestClass]
    public sealed class ResenasUT
    {
        private ResenasNegocio? negocio;
        private Resenas? entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Consultar();
            ValidarId();
            ConsultarPorCliente();
            Modificar();

        }

        private void Consultar()
        {
            negocio = new ResenasNegocio();
            var lista = negocio.Consultar();

            if (lista.Count > 0)
            {
                entidad = lista.First();
                return;
            }

            throw new Exception("Error consultando reseñas: lista vacía");
        }

        private void ConsultarPorCliente()
        {
            negocio = new ResenasNegocio();

            var lista = negocio.ConsultarPorCliente(entidad!.Clientes);

            if (lista.Count > 0)
                return;

            throw new Exception($"No se encontraron reseñas para el cliente con id: {entidad.Clientes}");
        }

        private void ValidarId()
        {
            negocio = new ResenasNegocio();

            bool existe = negocio.ValidarId(entidad!.Id);

            if (existe)
                return;

            throw new Exception("El ID de la reseña no es válido");
        }


        private void Modificar()
        {
            negocio = new ResenasNegocio();

            entidad!.Calificacion = 5;
            entidad.Comentario = "Comentario modificado test";
            entidad.TipoServicio = "Venta";

            var resultado = negocio.Modificar(entidad);

            if (resultado.Calificacion == 5 &&
                resultado.Comentario == "Comentario modificado test" &&
                resultado.TipoServicio == "Venta")
                return;

            throw new Exception("No se modificó la reseña");
        }

    }
}