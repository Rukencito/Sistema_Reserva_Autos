using Lib_Negocio_Autos.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface IFacturasPresentacion
    {
        List<Facturas> Consultar();
        Facturas Guardar(Facturas entidad);
        Facturas Modificar(Facturas entidad);
        Facturas Eliminar(Facturas entidad);
    }
}
