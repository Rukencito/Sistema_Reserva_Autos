using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class SucursalesPresentacion : ISucursalesPresentacion
    {
        private IComunicaciones? iComunicaciones;
        private readonly string _usuarioSesion;

        public SucursalesPresentacion(string usuarioSesion = "Sistema")
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

        public List<Sucursales> Consultar()
        {
            var datos = ConUrl("http://localhost:5108/Sucursales/Consultar");

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Sucursales>();

            return JsonConvert.DeserializeObject<List<Sucursales>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Sucursales Guardar(Sucursales entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = ConUrl("http://localhost:5108/Sucursales/Guardar");
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPost(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Sucursales();

            return JsonConvert.DeserializeObject<Sucursales>(
                respuesta["Valor"].ToString()!)!;
        }

        public Sucursales Modificar(Sucursales entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = ConUrl("http://localhost:5108/Sucursales/Modificar");
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Sucursales();

            return JsonConvert.DeserializeObject<Sucursales>(
                respuesta["Valor"].ToString()!)!;
        }

        public Sucursales Eliminar(Sucursales entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = ConUrl("http://localhost:5108/Sucursales/Eliminar");
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Sucursales();

            return JsonConvert.DeserializeObject<Sucursales>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<Sucursales> ConsultarPorCiudad(string ciudad)
        {
            var datos = ConUrl("http://localhost:5108/Sucursales/ConsultarPorCiudad");
            datos["Ciudad"] = ciudad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;
            if (!respuesta.ContainsKey("Valor"))
                return new List<Sucursales>();
            return JsonConvert.DeserializeObject<List<Sucursales>>(
                respuesta["Valor"].ToString()!)!;
        }
    }

}

