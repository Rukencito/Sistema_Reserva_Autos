using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class SegurosPresentacion : ISegurosPresentacion
    {
        private IComunicaciones? iComunicaciones;
        private readonly string _usuarioSesion;

        public SegurosPresentacion(string usuarioSesion = "Sistema")
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

        public List<Seguros> Consultar()
        {
            var datos = ConUrl("http://localhost:5108/Seguros/Consultar");

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Seguros>();

            return JsonConvert.DeserializeObject<List<Seguros>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Seguros Guardar(Seguros entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = ConUrl("http://localhost:5108/Seguros/Guardar");
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPost(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Seguros();

            return JsonConvert.DeserializeObject<Seguros>(
                respuesta["Valor"].ToString()!)!;
        }

        public Seguros Modificar(Seguros entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = ConUrl("http://localhost:5108/Seguros/Modificar");
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Seguros();

            return JsonConvert.DeserializeObject<Seguros>(
                respuesta["Valor"].ToString()!)!;
        }

        public Seguros Eliminar(Seguros entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = ConUrl("http://localhost:5108/Seguros/Eliminar");
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Seguros();

            return JsonConvert.DeserializeObject<Seguros>(
                respuesta["Valor"].ToString()!)!;
        }
        public Seguros ConsultarPorId(int id)
        {
            var datos = ConUrl("http://localhost:5108/Seguros/ConsultarPorId");
            datos["Id"] = id;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Seguros();

            return JsonConvert.DeserializeObject<Seguros>(
                respuesta["Valor"].ToString()!)!;
        }
    }

}


