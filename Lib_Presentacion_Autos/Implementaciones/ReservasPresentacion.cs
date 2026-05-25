using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class ReservasPresentacion : IReservasPresentacion
    {
        private IComunicaciones? iComunicaciones;

        public List<Reservas> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Reservas/Consultar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Reservas>();

            return JsonConvert.DeserializeObject<List<Reservas>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Reservas Guardar(Reservas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Reservas/Guardar";
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPost(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Reservas();

            return JsonConvert.DeserializeObject<Reservas>(
                respuesta["Valor"].ToString()!)!;
        }

        public Reservas Modificar(Reservas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Reservas/Modificar";
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Reservas();

            return JsonConvert.DeserializeObject<Reservas>(
                respuesta["Valor"].ToString()!)!;
        }

        public Reservas Eliminar(Reservas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Reservas/Eliminar";
            datos["Entidad"] = entidad;
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Reservas();

            return JsonConvert.DeserializeObject<Reservas>(
                respuesta["Valor"].ToString()!)!;
        }
        public bool ValidarReservaDuplicada(int autoId, int clienteId, DateTime fechaVencimiento)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Reservas/ValidarReservaDuplicada";
            datos["autoId"]= autoId;
            datos["clienteId"] = clienteId;
            datos["fechaVencimiento"] = fechaVencimiento;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return false;

            return Convert.ToBoolean(respuesta["Valor"].ToString());
        }

        public Reservas CambiarEstado(int reservaId, string nuevoEstado)
        {
            if (reservaId == 0)
                throw new Exception("No se ha guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Reservas/CambiarEstado";
            datos["reservaId"] = reservaId;
            datos["nuevoEstado"] = nuevoEstado;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Reservas();

            return JsonConvert.DeserializeObject<Reservas>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<Reservas> ConsultarPorCliente(int clienteId)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "http://localhost:5108/Reservas/ConsultarPorCliente";
            datos["clienteId"] = clienteId;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Reservas>();

            return JsonConvert.DeserializeObject<List<Reservas>>(
                respuesta["Valor"].ToString()!)!;
        }


    }

}
