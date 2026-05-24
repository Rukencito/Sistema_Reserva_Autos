using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class DetallesFacturaPresentacion : IDetallesFacturaPresentacion
    {
        private IComunicaciones? iComunicaciones;

        public List<DetallesFactura> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5188/DetallesFactura/Consultar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<DetallesFactura>();

            return JsonConvert.DeserializeObject<List<DetallesFactura>>(
                respuesta["Valor"].ToString()!)!;
        }

        public DetallesFactura Guardar(DetallesFactura entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5188/DetallesFactura/Guardar";
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPost(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new DetallesFactura();

            return JsonConvert.DeserializeObject<DetallesFactura>(
                respuesta["Valor"].ToString()!)!;
        }

        public DetallesFactura Modificar(DetallesFactura entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5188/DetallesFactura/Modificar";
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new DetallesFactura();

            return JsonConvert.DeserializeObject<DetallesFactura>(
                respuesta["Valor"].ToString()!)!;
        }

        public DetallesFactura Eliminar(DetallesFactura entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5188/DetallesFactura/Eliminar";
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new DetallesFactura();

            return JsonConvert.DeserializeObject<DetallesFactura>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}

