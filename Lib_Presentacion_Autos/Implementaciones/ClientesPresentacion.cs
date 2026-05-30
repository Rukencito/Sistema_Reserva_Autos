using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class ClientesPresentacion : IClientesPresentacion
    {
        private IComunicaciones? iComunicaciones;
        private readonly string _usuarioSesion;

        public ClientesPresentacion(string usuarioSesion = "Sistema")
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

        public List<Clientes> Consultar()
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Clientes/Consultar");

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Clientes>();

            return JsonConvert.DeserializeObject<List<Clientes>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Clientes Guardar(Clientes entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("El cliente ya fue guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Clientes/Guardar");
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarPost(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Clientes();

            return JsonConvert.DeserializeObject<Clientes>(
                respuesta["Valor"].ToString()!)!;
        }

        public Clientes Modificar(Clientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El cliente no ha sido guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Clientes/Modificar");
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Clientes();

            return JsonConvert.DeserializeObject<Clientes>(
                respuesta["Valor"].ToString()!)!;
        }

        public Clientes Eliminar(Clientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El cliente no ha sido guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Clientes/Eliminar");
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Clientes();

            return JsonConvert.DeserializeObject<Clientes>(
                respuesta["Valor"].ToString()!)!;
        }

        public Clientes ConsultarPorCedula(string cedula)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Clientes/ConsultarPorCedula?cedula=" + cedula);

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Clientes();

            return JsonConvert.DeserializeObject<Clientes>(
                respuesta["Valor"].ToString()!)!;
        }

        public Clientes AgregarPuntosFidelidad(int clienteId, int puntos)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Clientes/AgregarPuntosFidelidad");
            datos["Entidad"] = new { clienteId, puntos };

            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Clientes();

            return JsonConvert.DeserializeObject<Clientes>(
                respuesta["Valor"].ToString()!)!;
        }

        public bool TieneLicencia(int clienteId)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Clientes/TieneLicencia?clienteId=" + clienteId;

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return false;

            return JsonConvert.DeserializeObject<bool>(
                respuesta["Valor"].ToString()!);
        }
    }
}