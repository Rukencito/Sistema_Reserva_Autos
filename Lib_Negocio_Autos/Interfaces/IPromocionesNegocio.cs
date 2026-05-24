using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IPromocionesNegocio
    {
        List<Promociones> Consultar();
        Promociones Guardar(Promociones entidad);
        Promociones Eliminar(Promociones entidad);
        Promociones Modificar(Promociones entidad);
        bool ValidarId(int id);
        void ValidarDatos(Promociones entidad);
    }
}
