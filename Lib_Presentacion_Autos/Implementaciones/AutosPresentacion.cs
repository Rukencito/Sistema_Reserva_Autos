using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class AutosPresentacion : IAutosPresentacion
    {
        private IComunicaciones? iComunicaciones;

        public List<Autos> Consultar()
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Autos/Consultar";

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Autos>();

            return JsonConvert.DeserializeObject<List<Autos>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Autos Guardar(Autos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("El auto ya fue guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Autos/Guardar";
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarPost(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Autos();

            return JsonConvert.DeserializeObject<Autos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Autos Modificar(Autos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El auto no ha sido guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Autos/Modificar";
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Autos();

            return JsonConvert.DeserializeObject<Autos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Autos Eliminar(Autos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El auto no ha sido guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Autos/Eliminar";
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Autos();

            return JsonConvert.DeserializeObject<Autos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Autos ConsultarPorPlaca(string placa)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/ConsultarPorPlaca?placa=" + placa;

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Autos();

            return JsonConvert.DeserializeObject<Autos>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<Autos> ConsultarPorMarca(string marca)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Autos/ConsultarPorMarca?marca=" + marca;

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Autos>();

            return JsonConvert.DeserializeObject<List<Autos>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<Autos> ConsultarPorModelo(string modelo)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Autos/ConsultarPorModelo?modelo=" + modelo;

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Autos>();

            return JsonConvert.DeserializeObject<List<Autos>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<Autos> ConsultarDisponibles()
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Autos/ConsultarDisponibles";

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Autos>();

            return JsonConvert.DeserializeObject<List<Autos>>(
                respuesta["Valor"].ToString()!)!;
        }

        public bool VerificarDisponibilidad(string placa)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Autos/VerificarDisponibilidad?placa=" + placa;

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return false;

            return JsonConvert.DeserializeObject<bool>(
                respuesta["Valor"].ToString()!);
        }

        public bool CambiarEstado(string placa, bool nuevoEstado)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = new Dictionary<string, object>();

            datos["Url"] = "http://localhost:5108/Autos/CambiarEstado";
            datos["Entidad"] = new { placa, nuevoEstado };

            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return false;

            return JsonConvert.DeserializeObject<bool>(
                respuesta["Valor"].ToString()!);
        }
    }
}