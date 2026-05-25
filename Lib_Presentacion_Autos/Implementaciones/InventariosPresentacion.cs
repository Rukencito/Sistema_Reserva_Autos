using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class InventariosPresentacion : IInventariosPresentacion
    {
        private IComunicaciones? iComunicaciones;

        public List<Inventarios> Consultar()
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Inventarios/Consultar";

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
                throw new Exception("El inventario ya fue guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Inventarios/Guardar";
            datos["Entidad"] = entidad;

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
                throw new Exception("El inventario no ha sido guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Inventarios/Modificar";
            datos["Entidad"] = entidad;

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
                throw new Exception("El inventario no ha sido guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Inventarios/Eliminar";
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Inventarios();

            return JsonConvert.DeserializeObject<Inventarios>(
                respuesta["Valor"].ToString()!)!;
        }

        public Inventarios ConsultarPorId(int id)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Inventarios/ConsultarPorId?id=" + id;

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Inventarios();

            return JsonConvert.DeserializeObject<Inventarios>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<Inventarios> ConsultarPorUbicacion(string ubicacion)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Inventarios/ConsultarPorUbicacion?ubicacion=" + ubicacion;

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Inventarios>();

            return JsonConvert.DeserializeObject<List<Inventarios>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Inventarios AgregarStock(int inventarioId, int cantidad, decimal precioUnitario)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Inventarios/AgregarStock?inventarioId="
                + inventarioId + "&cantidad=" + cantidad + "&precioUnitario=" + precioUnitario;

            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Inventarios();

            return JsonConvert.DeserializeObject<Inventarios>(
                respuesta["Valor"].ToString()!)!;
        }

        public Inventarios ReducirStock(int inventarioId, int cantidad, decimal precioUnitario)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Inventarios/ReducirStock?inventarioId="
                + inventarioId + "&cantidad=" + cantidad + "&precioUnitario=" + precioUnitario;

            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Inventarios();

            return JsonConvert.DeserializeObject<Inventarios>(
                respuesta["Valor"].ToString()!)!;
        }

        public Inventarios RecalcularValorTotal(int inventarioId)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Inventarios/RecalcularValorTotal?inventarioId=" + inventarioId;

            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Inventarios();

            return JsonConvert.DeserializeObject<Inventarios>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}