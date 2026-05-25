using Lib_Negocio_Autos.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface IEmpleadosPresentacion
    {
        List<Empleados> Consultar();
        Empleados Guardar(Empleados entidad);
        Empleados Modificar(Empleados entidad);
        Empleados Eliminar(Empleados entidad);
        Empleados ConsultarPorCedula(string cedula);
        List<Empleados> ConsultarPorCargo(string cargo);
        List<Empleados> ConsultarPorSucursal(int sucursalId);
        decimal CalcularSalarioTotal(int empleadoId);
    }
}
