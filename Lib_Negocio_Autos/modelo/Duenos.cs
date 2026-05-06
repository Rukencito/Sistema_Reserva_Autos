using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Negocio_Autos.modelo
{
    public class Duenos
    {
        public int Id { get; set; }
        public int CantidadAutos { get; set; }
        public bool Estado { get; set; }
        public int PersonaId { get; set; }
        public Personas? Persona { get; set; }
      
    }
}
