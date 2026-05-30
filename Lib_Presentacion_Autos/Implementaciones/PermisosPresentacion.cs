using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class PermisosPresentacion : IPermisosPresentacion
    {
        private IComunicaciones? iComunicaciones;
        private readonly string _usuarioSesion;

        public PermisosPresentacion(string usuarioSesion = "Sistema")
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
        public List<Permisos> Consultar()
        {
            var datos = ConUrl("http://localhost:5108/Permisos/Consultar");

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Permisos>();

            return JsonConvert.DeserializeObject<List<Permisos>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Permisos Guardar(Permisos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = ConUrl("http://localhost:5108/Permisos/Guardar");
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPost(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Permisos();

            return JsonConvert.DeserializeObject<Permisos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Permisos Modificar(Permisos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = ConUrl("http://localhost:5108/Permisos/Modificar");
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Permisos();

            return JsonConvert.DeserializeObject<Permisos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Permisos Eliminar(Permisos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = ConUrl("http://localhost:5108/Permisos/Eliminar");
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Permisos();

            return JsonConvert.DeserializeObject<Permisos>(
                respuesta["Valor"].ToString()!)!;
        }

        public bool TienePermiso(int usuarioId, string nombrePermiso)
        {
            var datos = ConUrl("http://localhost:5108/Permisos/TienePermiso");
            datos["usuarioId"] = usuarioId;
            datos["nombrePermiso"] = nombrePermiso;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return false;

            return Convert.ToBoolean(respuesta["Valor"].ToString());

        }

        public bool TienePermisoPorCorreo(string correo, string nombrePermiso)
        {
            var datos = ConUrl("http://localhost:5108/Permisos/TienePermisoPorCorreo");
            datos["Correo"] = correo;
            datos["nombrePermiso"] = nombrePermiso;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return false;

            return Convert.ToBoolean(respuesta["Valor"].ToString());
        }

        public bool PermisoExisteEnRol(string nombrePermiso, int rolId)
        {
            var datos = ConUrl("http://localhost:5108/Permisos/PermisoExisteEnRol");
            datos["nombrePermiso"] = nombrePermiso;
            datos["rolId"] = rolId;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return false;

            return Convert.ToBoolean(respuesta["Valor"].ToString());
        }
    }

}

