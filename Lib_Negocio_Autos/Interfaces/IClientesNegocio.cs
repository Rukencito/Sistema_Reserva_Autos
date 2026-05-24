using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IClientesNegocio
    {
        List<Clientes> Consultar();
        Clientes Guardar(Clientes entidad);
        Clientes Eliminar(Clientes entidad);
        Clientes Modificar(Clientes entidad);
        bool ValidarCedula(string cedula);
        Clientes ConsultarPorCedula(string cedula);
        Clientes AgregarPuntosFidelidad(int clienteId, int puntos);
        bool TieneLicencia(int clienteId);
        void ValidarDatos(Clientes entidad);
    }
}
