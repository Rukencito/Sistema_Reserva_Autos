using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class AuditoriasPresentacion : IAuditoriasPresentacion
    {
        private IComunicaciones? iComunicaciones;
        private readonly string _usuarioSesion;

        public AuditoriasPresentacion(string usuarioSesion = "Sistema")
        {
            _usuarioSesion = usuarioSesion;
        }

        private Dictionary<string, object> ConUrl(string url)
        {
            return new Dictionary<string, object>
            {
                ["Url"] = url,
                ["X-Usuario"] = _usuarioSesion
            };
        }

        public List<Auditorias> Consultar()
        {
            var datos = ConUrl("http://localhost:5108/Auditorias/Consultar");

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Auditorias>();

            return JsonConvert.DeserializeObject<List<Auditorias>>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}