using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;
using System.Runtime.Intrinsics.Arm;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class AlquileresPresentacion : IAlquileresPresentacion
    {
        private IComunicaciones? iComunicaciones;
        private readonly string _usuarioSesion;

        public AlquileresPresentacion(string usuarioSesion = "Sistema")
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

        public List<Alquileres> Consultar()
        {
            var datos = ConUrl("http://localhost:5108/Alquileres/Consultar");

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Alquileres>();

            return JsonConvert.DeserializeObject<List<Alquileres>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Alquileres Guardar(Alquileres entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = ConUrl("http://localhost:5108/Alquileres/Guardar");
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPost(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Alquileres();

            return JsonConvert.DeserializeObject<Alquileres>(
                respuesta["Valor"].ToString()!)!;
        }

        public Alquileres Modificar(Alquileres entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = ConUrl("http://localhost:5108/Alquileres/Modificar");
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Alquileres();

            return JsonConvert.DeserializeObject<Alquileres>(
                respuesta["Valor"].ToString()!)!;
        }

        public Alquileres Eliminar(Alquileres entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = ConUrl("http://localhost:5108/Alquileres/Eliminar");
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Alquileres();

            return JsonConvert.DeserializeObject<Alquileres>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<Alquileres> ConsultarEstadoAlquiler(bool estadoAlquiler)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Alquileres/ConsultarEstadoAlquiler?estadoAlquiler=" + estadoAlquiler);

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Alquileres>();

            return JsonConvert.DeserializeObject<List<Alquileres>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<Alquileres> ConsultarAlquileresPorCliente(int clienteId)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Alquileres/ConsultarAlquileresPorCliente?clienteId=" 
                + clienteId);
            var task = this.iComunicaciones.Ejecutar(datos)!;

            task.Wait();
            var respuesta = task.Result;
            if (!respuesta.ContainsKey("Valor"))
                return new List<Alquileres>();

            return JsonConvert.DeserializeObject<List<Alquileres>>(
                respuesta["Valor"].ToString()!)!;
        }

        public bool ExisteCruceDeFechas(int autoId, DateTime fechaInicio, DateTime fechaFin)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Alquileres/ExisteCruceDeFechas?autoId=" + autoId +
                                       "&fechaInicio=" + fechaInicio.ToString("yyyy-MM-dd") +
                                       "&fechaFin=" + fechaFin.ToString("yyyy-MM-dd")); var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;
            if (!respuesta.ContainsKey("Valor"))
                return false;

            return JsonConvert.DeserializeObject<bool>(
                respuesta["Valor"].ToString()!)!;
        }

        public decimal CalcularTotalPrecio(decimal precioAlquiler, DateTime fechaInicio, DateTime fechaFin)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Alquileres/CalcularTotalPrecio?precioAlquiler=" + precioAlquiler +
                                       "&fechaInicio=" + fechaInicio.ToString("yyyy-MM-dd") +
                                       "&fechaFin=" + fechaFin.ToString("yyyy-MM-dd")); var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;
            if (!respuesta.ContainsKey("Valor"))
                return 0;

            return JsonConvert.DeserializeObject<decimal>(
                respuesta["Valor"].ToString()!)!;   
        }
    }
}

