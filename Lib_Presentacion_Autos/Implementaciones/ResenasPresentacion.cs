using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class ResenasPresentacion : IResenasPresentacion
    {
        private IComunicaciones? iComunicaciones;

        public List<Resenas> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Resenas/Consultar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Resenas>();

            return JsonConvert.DeserializeObject<List<Resenas>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Resenas Guardar(Resenas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Resenas/Guardar";
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPost(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Resenas();

            return JsonConvert.DeserializeObject<Resenas>(
                respuesta["Valor"].ToString()!)!;
        }

        public Resenas Modificar(Resenas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Resenas/Modificar";
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Resenas();

            return JsonConvert.DeserializeObject<Resenas>(
                respuesta["Valor"].ToString()!)!;
        }

        public Resenas Eliminar(Resenas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Resenas/Eliminar";
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Resenas();

            return JsonConvert.DeserializeObject<Resenas>(
                respuesta["Valor"].ToString()!)!;
        }
        public List<Resenas> ConsultarPorCliente(int idCliente)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Resenas/ConsultarPorCliente";
            datos["idCliente"] = idCliente;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Resenas>();

            return JsonConvert.DeserializeObject<List<Resenas>>(
                respuesta["Valor"].ToString()!)!;
        }
    }

}

