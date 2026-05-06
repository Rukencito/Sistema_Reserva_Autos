using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class Autos
    {
		public int Id { get; set; }
		public string? Placa { get; set; }
		public string? Marca { get; set; }
		public int Año { get; set; }
		public string? Modelo { get; set; }
		public bool Estado { get; set; }
		public string? Color { get; set; }

		public int ParqueaderoId { get; set; }
		public int DuenoId { get; set; }
		public int SucursalId { get; set; }
		public int InventarioId { get; set; }
		public int VentaId { get; set; }

		public Parqueaderos? Parqueadero { get; set; }
		public Duenos? Dueno { get; set; }
		public Sucursales? Sucursal { get; set; }
		public Inventarios? Inventario { get; set; }
		public Ventas? Venta { get; set; }

    }
}
