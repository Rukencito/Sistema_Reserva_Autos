using Lib_Negocio_Autos.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface IGarantiasPresentacion
    {
        List<Garantias> Consultar();
        Garantias Guardar(Garantias entidad);
        Garantias Modificar(Garantias entidad);
        Garantias Eliminar(Garantias entidad);
        Garantias ConsultarPorId(int id);
        List<Garantias> ConsultarPorAuto(int autoId);
        bool TieneGarantiaVigente(int autoId);
    }
}
