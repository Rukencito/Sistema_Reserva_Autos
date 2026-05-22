using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class EmpleadosPresentacion : IEmpleadosPresentacion
    {
        private IComunicaciones? iComunicaciones;

        public List<Empleados> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5188/Empleados/Consultar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Empleados>();

            return JsonConvert.DeserializeObject<List<Empleados>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Empleados Guardar(Empleados entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5188/Empleados/Guardar";
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPost(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Empleados();

            return JsonConvert.DeserializeObject<Empleados>(
                respuesta["Valor"].ToString()!)!;
        }

        public Empleados Modificar(Empleados entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5188/Empleados/Modificar";
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Empleados();

            return JsonConvert.DeserializeObject<Empleados>(
                respuesta["Valor"].ToString()!)!;
        }

        public Empleados Eliminar(Empleados entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5188/Empleados/Eliminar";
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Empleados();

            return JsonConvert.DeserializeObject<Empleados>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}

