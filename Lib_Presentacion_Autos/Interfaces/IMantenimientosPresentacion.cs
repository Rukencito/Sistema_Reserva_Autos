using Lib_Negocio_Autos.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface IMantenimientosPresentacion
    {
        List<Mantenimientos> Consultar();
        Mantenimientos Guardar(Mantenimientos entidad);
        Mantenimientos Modificar(Mantenimientos entidad);
        Mantenimientos Eliminar(Mantenimientos entidad);
        Mantenimientos ConsultarPorId(int id);
        List<Mantenimientos> ConsultarPorAuto(int autoId);
        List<Mantenimientos> ConsultarPorTaller(int tallerId);
        Mantenimientos FinalizarMantenimiento(int mantenimientoId);
    }
}
