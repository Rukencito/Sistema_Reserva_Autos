using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class DevolucionesPresentacion : IDevolucionesPresentacion
    {
        private IComunicaciones? iComunicaciones;

        public List<Devoluciones> Consultar()
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Devoluciones/Consultar";

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Devoluciones>();

            return JsonConvert.DeserializeObject<List<Devoluciones>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Devoluciones Guardar(Devoluciones entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("La devolucion ya fue guardada");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Devoluciones/Guardar";
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarPost(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Devoluciones();

            return JsonConvert.DeserializeObject<Devoluciones>(
                respuesta["Valor"].ToString()!)!;
        }

        public Devoluciones Modificar(Devoluciones entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("La devolucion no ha sido guardada");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Devoluciones/Modificar";
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Devoluciones();

            return JsonConvert.DeserializeObject<Devoluciones>(
                respuesta["Valor"].ToString()!)!;
        }

        public Devoluciones Eliminar(Devoluciones entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("La devolucion no ha sido guardada");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Devoluciones/Eliminar";
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Devoluciones();

            return JsonConvert.DeserializeObject<Devoluciones>(
                respuesta["Valor"].ToString()!)!;
        }

        public Devoluciones ConsultarPorAlquiler(int idAlquiler)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Devoluciones/ConsultarPorAlquiler?idAlquiler=" + idAlquiler;

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Devoluciones();

            return JsonConvert.DeserializeObject<Devoluciones>(
                respuesta["Valor"].ToString()!)!;
        }

        public Devoluciones ConsultarPorId(int id)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Devoluciones/ConsultarPorId?id=" + id;

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Devoluciones();

            return JsonConvert.DeserializeObject<Devoluciones>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}