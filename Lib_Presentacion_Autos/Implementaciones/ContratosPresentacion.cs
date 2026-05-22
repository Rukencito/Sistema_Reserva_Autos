using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class ContratosPresentacion : IContratosPresentacion
    {
        private IComunicaciones? iComunicaciones;

        public List<Contratos> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5188/Contratos/Consultar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Contratos>();

            return JsonConvert.DeserializeObject<List<Contratos>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Contratos Guardar(Contratos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5188/Contratos/Guardar";
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPost(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Contratos();

            return JsonConvert.DeserializeObject<Contratos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Contratos Modificar(Contratos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5188/Contratos/Modificar";
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Contratos();

            return JsonConvert.DeserializeObject<Contratos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Contratos Eliminar(Contratos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5188/Contratos/Eliminar";
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Contratos();

            return JsonConvert.DeserializeObject<Contratos>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}

