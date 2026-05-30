using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class GarantiasPresentacion : IGarantiasPresentacion
    {
        private IComunicaciones? iComunicaciones;
        private readonly string _usuarioSesion;

        public GarantiasPresentacion(string usuarioSesion = "Sistema")
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
        public List<Garantias> Consultar()
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Garantias/Consultar");

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Garantias>();

            return JsonConvert.DeserializeObject<List<Garantias>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Garantias Guardar(Garantias entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("La garantía ya fue guardada");

            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Garantias/Guardar");
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarPost(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Garantias();

            return JsonConvert.DeserializeObject<Garantias>(
                respuesta["Valor"].ToString()!)!;
        }

        public Garantias Modificar(Garantias entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("La garantía no ha sido guardada");

            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Garantias/Modificar");
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Garantias();

            return JsonConvert.DeserializeObject<Garantias>(
                respuesta["Valor"].ToString()!)!;
        }

        public Garantias Eliminar(Garantias entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("La garantía no ha sido guardada");

            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Garantias/Eliminar");
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Garantias();

            return JsonConvert.DeserializeObject<Garantias>(
                respuesta["Valor"].ToString()!)!;
        }

        public Garantias ConsultarPorId(int id)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Garantias/ConsultarPorId?id=" + id);

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Garantias();

            return JsonConvert.DeserializeObject<Garantias>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<Garantias> ConsultarPorAuto(int autoId)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Garantias/ConsultarPorAuto?autoId=" + autoId);

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Garantias>();

            return JsonConvert.DeserializeObject<List<Garantias>>(
                respuesta["Valor"].ToString()!)!;
        }

        public bool TieneGarantiaVigente(int autoId)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Garantias/TieneGarantiaVigente?autoId=" + autoId);

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