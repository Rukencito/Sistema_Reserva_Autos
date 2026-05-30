using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class ParqueaderosPresentacion : IParqueaderosPresentacion
    {
        private IComunicaciones? iComunicaciones;
        private readonly string _usuarioSesion;

        public ParqueaderosPresentacion(string usuarioSesion = "Sistema")
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
        public List<Parqueaderos> Consultar()
        {
            var datos = ConUrl("http://localhost:5108/Parqueaderos/Consultar");

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Parqueaderos>();

            return JsonConvert.DeserializeObject<List<Parqueaderos>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Parqueaderos Guardar(Parqueaderos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = ConUrl("http://localhost:5108/Parqueaderos/Guardar");
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPost(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Parqueaderos();

            return JsonConvert.DeserializeObject<Parqueaderos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Parqueaderos Modificar(Parqueaderos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = ConUrl("http://localhost:5108/Parqueaderos/Modificar");
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Parqueaderos();

            return JsonConvert.DeserializeObject<Parqueaderos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Parqueaderos Eliminar(Parqueaderos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = ConUrl("http://localhost:5108/Parqueaderos/Eliminar");
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Parqueaderos();

            return JsonConvert.DeserializeObject<Parqueaderos>(
                respuesta["Valor"].ToString()!)!;
        }
        public Parqueaderos ConsultarPorId(int id)
        {
            var datos = ConUrl("http://localhost:5108/Parqueaderos/ConsultarPorId?id=" + id);

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Parqueaderos();

            return JsonConvert.DeserializeObject<Parqueaderos>(
                respuesta["Valor"].ToString()!)!;
        }

        public int ContarAutosEnParqueadero(int parqueaderoId)
        {
            var datos = ConUrl("http://localhost:5108/Parqueaderos/ContarAutosEnParqueadero");
            datos["ParqueaderoId"] = parqueaderoId;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return 0;

            return Convert.ToInt32(respuesta["Valor"].ToString());
        }
        public int ConsultarEspaciosDisponibles(int parqueaderoId)
        {
            var datos = ConUrl("http://localhost:5108/Parqueaderos/ConsultarEspaciosDisponibles");
            datos["ParqueaderoId"] = parqueaderoId;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return 0;
            return Convert.ToInt32(respuesta["Valor"].ToString());
        }
      public bool TieneEspacioDisponible(int parqueaderoId)
        {
            var datos = ConUrl("http://localhost:5108/Parqueaderos/TieneEspacioDisponible");
            datos["ParqueaderoId"] = parqueaderoId;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return false;
            return Convert.ToBoolean(respuesta["Valor"].ToString());
        }

        public List<Parqueaderos> ConsultarConEspacioDisponible()
        {
            var datos = ConUrl("http://localhost:5108/Parqueaderos/ConsultarConEspacioDisponible");
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;
            if (!respuesta.ContainsKey("Valor"))
                return new List<Parqueaderos>();

            return JsonConvert.DeserializeObject<List<Parqueaderos>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<Autos> ConsultarAutosPorParqueadero(int parqueaderoId)
        {
            var datos = ConUrl("http://localhost:5108/Parqueaderos/ConsultarAutosPorParqueadero");
            datos["ParqueaderoId"] = parqueaderoId;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;
            if (!respuesta.ContainsKey("Valor"))
                return new List<Autos>();

            return JsonConvert.DeserializeObject<List<Autos>>(
                respuesta["Valor"].ToString()!)!;
        }

    }
}



