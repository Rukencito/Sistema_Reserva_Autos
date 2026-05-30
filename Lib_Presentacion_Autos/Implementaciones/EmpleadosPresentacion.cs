using Lib_Negocio_Autos.modelo;
using Lib_Presentacion_Autos.Implementaciones;
using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class EmpleadosPresentacion : IEmpleadosPresentacion
    {
        private IComunicaciones? iComunicaciones;
        private readonly string _usuarioSesion;

        public EmpleadosPresentacion(string usuarioSesion = "Sistema")
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
        public List<Empleados> Consultar()
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Empleados/Consultar");

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Empleados>();

            return JsonConvert.DeserializeObject<List<Empleados>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Empleados Guardar(Empleados entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("El empleado ya fue guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Empleados/Guardar");
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarPost(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Empleados();

            return JsonConvert.DeserializeObject<Empleados>(
                respuesta["Valor"].ToString()!)!;
        }

        public Empleados Modificar(Empleados entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El empleado no ha sido guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Empleados/Modificar");
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarPut(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Empleados();

            return JsonConvert.DeserializeObject<Empleados>(
                respuesta["Valor"].ToString()!)!;
        }

        public Empleados Eliminar(Empleados entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El empleado no ha sido guardado");

            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Empleados/Eliminar");
            datos["Entidad"] = entidad;

            var task = this.iComunicaciones.EjecutarDelete(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Empleados();

            return JsonConvert.DeserializeObject<Empleados>(
                respuesta["Valor"].ToString()!)!;
        }

        public Empleados ConsultarPorCedula(string cedula)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Empleados/ConsultarPorCedula?cedula=" + cedula);

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Empleados();

            return JsonConvert.DeserializeObject<Empleados>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<Empleados> ConsultarPorCargo(string cargo)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Empleados/ConsultarPorCargo?cargo=" + cargo);

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Empleados>();

            return JsonConvert.DeserializeObject<List<Empleados>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<Empleados> ConsultarPorSucursal(int sucursalId)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Empleados/ConsultarPorSucursal?sucursalId=" + sucursalId);

            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Empleados>();

            return JsonConvert.DeserializeObject<List<Empleados>>(
                respuesta["Valor"].ToString()!)!;
        }

        public decimal CalcularSalarioTotal(int empleadoId)
        {
            this.iComunicaciones = new Comunicaciones();
            var datos = ConUrl("http://localhost:5108/Empleados/CalcularSalarioTotal?empleadoId=" + empleadoId);

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