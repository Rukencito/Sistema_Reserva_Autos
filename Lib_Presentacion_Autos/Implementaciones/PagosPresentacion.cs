using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class PagosPresentacion : IPagosPresentacion
    {
        private IComunicaciones? iComunicaciones;

        public List<Pagos> Consultar()
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Pagos/Consultar";

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Pagos>();

            return JsonConvert.DeserializeObject<List<Pagos>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Pagos Guardar(Pagos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("El pago ya fue guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Pagos/Guardar";
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarPost(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Pagos();

            return JsonConvert.DeserializeObject<Pagos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Pagos Modificar(Pagos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El pago no ha sido guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Pagos/Modificar";
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Pagos();

            return JsonConvert.DeserializeObject<Pagos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Pagos Eliminar(Pagos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El pago no ha sido guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Pagos/Eliminar";
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Pagos();

            return JsonConvert.DeserializeObject<Pagos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Pagos ConsultarPorId(int id)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Pagos/ConsultarPorId?id=" + id;

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Pagos();

            return JsonConvert.DeserializeObject<Pagos>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<Pagos> ConsultarPorFactura(int facturaId)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Pagos/ConsultarPorFactura?facturaId=" + facturaId;

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Pagos>();

            return JsonConvert.DeserializeObject<List<Pagos>>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}