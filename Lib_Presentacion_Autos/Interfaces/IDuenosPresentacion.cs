using Lib_Negocio_Autos.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface IDuenosPresentacion
    {
        List<Duenos> Consultar();
        Duenos Guardar(Duenos entidad);
        Duenos Modificar(Duenos entidad);
        Duenos Eliminar(Duenos entidad);
        Duenos ConsultarPorCedula(string cedula);
        bool VerificarEstadoDueno(int duenoId);
        Duenos AgregarAuto(int duenoId);
        Duenos QuitarAuto(int duenoId);
    }
}
