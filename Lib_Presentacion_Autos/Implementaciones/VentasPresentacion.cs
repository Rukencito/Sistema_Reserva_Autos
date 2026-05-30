using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class VentasPresentacion : IVentasPresentacion
    {
        private IComunicaciones? iComunicaciones;
        private readonly string _usuarioSesion;

        public VentasPresentacion(string usuarioSesion = "Sistema")
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

        public List<Ventas> Consultar()
        {
            var datos = ConUrl("http://localhost:5108/Ventas/Consultar");

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Ventas>();

            return JsonConvert.DeserializeObject<List<Ventas>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Ventas Guardar(Ventas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = ConUrl("http://localhost:5108/Ventas/Guardar");
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPost(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Ventas();

            return JsonConvert.DeserializeObject<Ventas>(
                respuesta["Valor"].ToString()!)!;
        }

        public Ventas Modificar(Ventas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = ConUrl("http://localhost:5108/Ventas/Modificar");
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Ventas();

            return JsonConvert.DeserializeObject<Ventas>(
                respuesta["Valor"].ToString()!)!;
        }

        public Ventas Eliminar(Ventas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = ConUrl("http://localhost:5108/Ventas/Eliminar");
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Ventas();

            return JsonConvert.DeserializeObject<Ventas>(
                respuesta["Valor"].ToString()!)!;
        }
        public List<Ventas> ConsultarPorCliente(int idCliente)
        {
            var datos = ConUrl("http://localhost:5108/Ventas/ConsultarPorCliente?idCliente=" + idCliente);

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Ventas>();

            return JsonConvert.DeserializeObject<List<Ventas>>(
                respuesta["Valor"].ToString()!)!;

        }
    }

}

