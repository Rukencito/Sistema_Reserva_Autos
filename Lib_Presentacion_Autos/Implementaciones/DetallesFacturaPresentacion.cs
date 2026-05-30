using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class DetallesFacturaPresentacion : IDetallesFacturaPresentacion
    {
        private IComunicaciones? iComunicaciones;
        private readonly string _usuarioSesion;

        public DetallesFacturaPresentacion(string usuarioSesion = "Sistema")
        {
            _usuarioSesion = usuarioSesion;
        }

        private Dictionary<string, object> ConUrl(string url)
        {
            return new Dictionary<string, object>
            {
                ["Url"] = url,
                ["X-Usuario"] = _usuarioSesion
            };
        }
        public List<DetallesFactura> Consultar()
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/DetallesFactura/Consultar");

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
                throw new Exception("El detalle ya fue guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/DetallesFactura/Guardar");
            datos["Entidad"] = entidad;

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
                throw new Exception("El detalle no ha sido guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/DetallesFactura/Modificar");
            datos["Entidad"] = entidad;

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
                throw new Exception("El detalle no ha sido guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/DetallesFactura/Eliminar");
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new DetallesFactura();

            return JsonConvert.DeserializeObject<DetallesFactura>(
                respuesta["Valor"].ToString()!)!;
        }

        public DetallesFactura ConsultarPorId(int id)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/DetallesFactura/ConsultarPorId?id=" + id);

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new DetallesFactura();

            return JsonConvert.DeserializeObject<DetallesFactura>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<DetallesFactura> ConsultarPorFactura(int facturaId)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/DetallesFactura/ConsultarPorFactura?facturaId=" + facturaId);

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<DetallesFactura>();

            return JsonConvert.DeserializeObject<List<DetallesFactura>>(
                respuesta["Valor"].ToString()!)!;
        }

        public decimal CalcularSubtotalPorFactura(int facturaId)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/DetallesFactura/CalcularSubtotalPorFactura?facturaId=" + facturaId);

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return 0;

            return JsonConvert.DeserializeObject<decimal>(
                respuesta["Valor"].ToString()!);
        }
    }
}