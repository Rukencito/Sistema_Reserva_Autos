using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class InventariosPresentacion : IInventariosPresentacion
    {
        private IComunicaciones? iComunicaciones;

        public List<Inventarios> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5188/Inventarios/Consultar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Inventarios>();

            return JsonConvert.DeserializeObject<List<Inventarios>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Inventarios Guardar(Inventarios entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5188/Inventarios/Guardar";
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPost(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Inventarios();

            return JsonConvert.DeserializeObject<Inventarios>(
                respuesta["Valor"].ToString()!)!;
        }

        public Inventarios Modificar(Inventarios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5188/Inventarios/Modificar";
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Inventarios();

            return JsonConvert.DeserializeObject<Inventarios>(
                respuesta["Valor"].ToString()!)!;
        }

        public Inventarios Eliminar(Inventarios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5188/Inventarios/Eliminar";
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Inventarios();

            return JsonConvert.DeserializeObject<Inventarios>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}

