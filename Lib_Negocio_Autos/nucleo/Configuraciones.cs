using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Negocio_Autos.nucleo
{
    public class Configuraciones
    {
        public static string obtener(string clave)
        {
            return "server=localhost;database=db_SistemaAutos;Integrated Security=True;TrustServerCertificate=true;";
        }
    }
}
