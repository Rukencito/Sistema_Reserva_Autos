using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IGarantiasNegocio
    {
        List<Garantias> Consultar();
        Garantias Guardar(Garantias entidad);
        Garantias Eliminar(Garantias entidad);
        Garantias Modificar(Garantias entidad);
        bool ValidarId(int id);
        Garantias ConsultarPorId(int id);
        List<Garantias> ConsultarPorAuto(int autoId);
        bool TieneGarantiaVigente(int autoId);
        void ValidarDatos(Garantias entidad);
    }
}
