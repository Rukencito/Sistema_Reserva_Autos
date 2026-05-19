using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface IComunicaciones
    {
        Task<Dictionary<string, object>> Ejecutar(Dictionary<string, object> datos);
        Task<Dictionary<string, object>> EjecutarPost(Dictionary<string, object> datos);
        Task<Dictionary<string, object>> EjecutarPut(Dictionary<string, object> datos);
        Task<Dictionary<string, object>> EjecutarDelete(Dictionary<string, object> datos);

    }
}
