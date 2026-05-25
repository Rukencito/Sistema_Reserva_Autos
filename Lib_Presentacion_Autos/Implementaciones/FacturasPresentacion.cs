using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class FacturasPresentacion : IFacturasPresentacion
    {
        private IComunicaciones? iComunicaciones;

        public List<Facturas> Consultar()
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Facturas/Consultar";

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Facturas>();

            return JsonConvert.DeserializeObject<List<Facturas>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Facturas Guardar(Facturas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("La factura ya fue guardada");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Facturas/Guardar";
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarPost(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Facturas();

            return JsonConvert.DeserializeObject<Facturas>(
                respuesta["Valor"].ToString()!)!;
        }

        public Facturas Modificar(Facturas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("La factura no ha sido guardada");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Facturas/Modificar";
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Facturas();

            return JsonConvert.DeserializeObject<Facturas>(
                respuesta["Valor"].ToString()!)!;
        }

        public Facturas Eliminar(Facturas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("La factura no ha sido guardada");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Facturas/Eliminar";
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Facturas();

            return JsonConvert.DeserializeObject<Facturas>(
                respuesta["Valor"].ToString()!)!;
        }

        public void CalcularTotal(Facturas id)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Facturas/CalcularTotal";
            datos["Entidad"] = id;

            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
        }

        public List<Facturas> ConsultarPorCliente(int id)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Facturas/ConsultarPorCliente?id=" + id;

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Facturas>();

            return JsonConvert.DeserializeObject<List<Facturas>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Facturas ConsultarPorId(int id)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Facturas/ConsultarPorId?id=" + id;

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Facturas();

            return JsonConvert.DeserializeObject<Facturas>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<Facturas> ConsultarPendientes()
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Facturas/ConsultarPendientes";

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Facturas>();

            return JsonConvert.DeserializeObject<List<Facturas>>(
                respuesta["Valor"].ToString()!)!;
        }

        public void MarcarComoPagada(int id)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Facturas/MarcarComoPagada?id=" + id;

            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
        }
    }
}