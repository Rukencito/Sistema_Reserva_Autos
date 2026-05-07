using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class Duenos
    {
        public int Id { get; set; }
        public int CantidadAutos { get; set; }
        public bool Estado { get; set; }
        public int Persona { get; set; }
        [ForeignKey("Persona")] public Personas? _Persona { get; set; }

        [NotMapped] public List<Autos>? Autos { get; set; }
      
    }
}
