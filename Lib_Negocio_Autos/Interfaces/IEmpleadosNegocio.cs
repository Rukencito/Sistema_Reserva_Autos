using Lib_Negocio_Autos.modelo;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IEmpleadosNegocio
    {
        List<Empleados> Consultar();
        Empleados Guardar(Empleados entidad);
        Empleados Eliminar(Empleados entidad);
        Empleados Modificar(Empleados entidad);
        bool ValidarId(int id);
        bool ValidarCedula(string cedula);
        Empleados ConsultarPorCedula(string cedula);
        List<Empleados> ConsultarPorCargo(string cargo);
        List<Empleados> ConsultarPorSucursal(int sucursalId);
        decimal CalcularSalarioTotal(int empleadoId);
        void ValidarDatos(Empleados entidad);
    }
}
