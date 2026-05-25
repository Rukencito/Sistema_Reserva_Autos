using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class ContratosPresentacion : IContratosPresentacion
    {
        private IComunicaciones? iComunicaciones;
        public List<Contratos> Consultar()
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Contratos/Consultar";

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
                throw new Exception("El contrato ya fue guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Contratos/Guardar";
            datos["Entidad"] = entidad;

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
                throw new Exception("El contrato no ha sido guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Contratos/Modificar";
            datos["Entidad"] = entidad;

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
                throw new Exception("El contrato no ha sido guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Contratos/Eliminar";
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Contratos();

            return JsonConvert.DeserializeObject<Contratos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Contratos ConsultarPorId(int id)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Contratos/ConsultarPorId?id=" + id;

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Contratos();

            return JsonConvert.DeserializeObject<Contratos>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<Contratos> ConsultarPorAlquiler(int alquilerId)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Contratos/ConsultarPorAlquiler?alquilerId=" + alquilerId;

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Contratos>();

            return JsonConvert.DeserializeObject<List<Contratos>>(
                respuesta["Valor"].ToString()!)!;
        }

    }
}