using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class DuenosPresentacion : IDuenosPresentacion
    {
        private IComunicaciones? iComunicaciones;

        public List<Duenos> Consultar()
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Duenos/Consultar";

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Duenos>();

            return JsonConvert.DeserializeObject<List<Duenos>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Duenos Guardar(Duenos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("La devolucion ya fue guardada");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Duenos/Guardar";
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarPost(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Duenos();

            return JsonConvert.DeserializeObject<Duenos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Duenos Modificar(Duenos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("La devolucion no ha sido guardada");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Duenos/Modificar";
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Duenos();

            return JsonConvert.DeserializeObject<Duenos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Duenos Eliminar(Duenos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("La devolucion no ha sido guardada");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Duenos/Eliminar";
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Duenos();

            return JsonConvert.DeserializeObject<Duenos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Duenos ConsultarPorCedula(string cedula)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Duenos/ConsultarPorCedula?cedula=" + cedula;
            
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;
            
            if (!respuesta.ContainsKey("Valor"))
                return new Duenos();
            
            return JsonConvert.DeserializeObject<Duenos>(
                respuesta["Valor"].ToString()!)!;
        }

        public bool VerificarEstadoDueno(int duenoId)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Duenos/VerificarEstadoDueno?duenoId=" + duenoId;

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return false;

            return JsonConvert.DeserializeObject<bool>(
                respuesta["Valor"].ToString()!);
        }

        public Duenos AgregarAuto(int duenoId)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Duenos/AgregarAuto?duenoId=" + duenoId;

            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Duenos();

            return JsonConvert.DeserializeObject<Duenos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Duenos QuitarAuto(int duenoId)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Duenos/QuitarAuto?duenoId=" + duenoId;

            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Duenos();

            return JsonConvert.DeserializeObject<Duenos>(
                respuesta["Valor"].ToString()!)!;
        }

    }
}