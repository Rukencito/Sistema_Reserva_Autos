using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class Ventas
    {
		public int Id { get; set; }
		public DateTime FechaVenta { get; set; }
		public double PrecioVenta { get; set; }
		public string? TipoPago { get; set; }
		public bool EstadoPago  { get; set; }

		public int Cliente { get; set; }
		public int Empleado { get; set; }

        [ForeignKey("Cliente")] public Clientes? _Cliente { get; set; }
        [ForeignKey("Empleado")] public Empleados? _Empleado { get; set; }

        [NotMapped] public List<Autos>? Auto { get; set; }
        [NotMapped] public List<Promociones>? Promocion { get; set; }

    }
}
