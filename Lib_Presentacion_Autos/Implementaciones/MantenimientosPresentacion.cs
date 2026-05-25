using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class MantenimientosPresentacion : IMantenimientosPresentacion
    {
        private IComunicaciones? iComunicaciones;

        public List<Mantenimientos> Consultar()
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Mantenimientos/Consultar";

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Mantenimientos>();

            return JsonConvert.DeserializeObject<List<Mantenimientos>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Mantenimientos Guardar(Mantenimientos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("El mantenimiento ya fue guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Mantenimientos/Guardar";
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarPost(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Mantenimientos();

            return JsonConvert.DeserializeObject<Mantenimientos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Mantenimientos Modificar(Mantenimientos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El mantenimiento no ha sido guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Mantenimientos/Modificar";
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Mantenimientos();

            return JsonConvert.DeserializeObject<Mantenimientos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Mantenimientos Eliminar(Mantenimientos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El mantenimiento no ha sido guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Mantenimientos/Eliminar";
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Mantenimientos();

            return JsonConvert.DeserializeObject<Mantenimientos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Mantenimientos ConsultarPorId(int id)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Mantenimientos/ConsultarPorId?id=" + id;

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Mantenimientos();

            return JsonConvert.DeserializeObject<Mantenimientos>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<Mantenimientos> ConsultarPorAuto(int autoId)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Mantenimientos/ConsultarPorAuto?autoId=" + autoId;

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Mantenimientos>();

            return JsonConvert.DeserializeObject<List<Mantenimientos>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<Mantenimientos> ConsultarPorTaller(int tallerId)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Mantenimientos/ConsultarPorTaller?tallerId=" + tallerId;

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Mantenimientos>();

            return JsonConvert.DeserializeObject<List<Mantenimientos>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Mantenimientos FinalizarMantenimiento(int mantenimientoId)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Mantenimientos/FinalizarMantenimiento?mantenimientoId=" + mantenimientoId;

            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Mantenimientos();

            return JsonConvert.DeserializeObject<Mantenimientos>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}