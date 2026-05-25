using Lib_Negocio_Autos.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface IVentasPresentacion
    {
        List<Ventas> Consultar();
        Ventas Guardar(Ventas entidad);
        Ventas Modificar(Ventas entidad);
        Ventas Eliminar(Ventas entidad);
        List<Ventas> ConsultarPorCliente(int idCliente);
    }
}
