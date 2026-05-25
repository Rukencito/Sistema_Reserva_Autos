using Lib_Negocio_Autos.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface IDetallesFacturaPresentacion
    {
        List<DetallesFactura> Consultar();
        DetallesFactura Guardar(DetallesFactura entidad);
        DetallesFactura Modificar(DetallesFactura entidad);
        DetallesFactura Eliminar(DetallesFactura entidad);
        DetallesFactura ConsultarPorId(int id);
        List<DetallesFactura> ConsultarPorFactura(int facturaId);
        decimal CalcularSubtotalPorFactura(int facturaId);
    }
}
