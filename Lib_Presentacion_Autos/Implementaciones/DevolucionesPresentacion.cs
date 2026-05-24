using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class DevolucionesPresentacion : IDevolucionesPresentacion
    {
        private IComunicaciones? iComunicaciones;

        public List<Devoluciones> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5188/Devoluciones/Consultar";

            this.iComunicaciones = new Comunicaciones();
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
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5188/Devoluciones/Guardar";
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
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
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5188/Devoluciones/Modificar";
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
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
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5188/Devoluciones/Eliminar";
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Devoluciones();

            return JsonConvert.DeserializeObject<Devoluciones>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}

