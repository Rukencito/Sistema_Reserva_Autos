using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class AlquileresNegocio : IAlquileresNegocio
    {
        private IConexion? iConexion;
        public List<Alquileres> Consultar()
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var lista = iConexion.Alquileres!.ToList();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo una consulta en Alquileres";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Consulta";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();

            return lista;
        }

        public Alquileres Guardar(Alquileres entidad)
        {

            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            iConexion.Alquileres!.Add(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo un guardado en Alquileres";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Guardado";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public Alquileres Eliminar(Alquileres entidad)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            ValidarDatos(entidad);
            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El registro no existe");
            }
            iConexion.Alquileres!.Remove(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se elimino un registro en Alquileres";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Eliminacion";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public Alquileres Modificar(Alquileres entidad)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            ValidarDatos(entidad);
            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El registro no existe");
            }
            iConexion.Alquileres!.Update(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se modifico un registro en Alquileres";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Modificacion";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public bool ValidarId(int id)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            var Alquiler = iConexion.Alquileres!.FirstOrDefault(a => a.Id == id);
            return Alquiler != null;
        }

        public void ValidarDatos(Alquileres entidad)
        {
            if (entidad == null)
            {
                throw new Exception("La información del auto es obligatoria");
            }

            if (entidad.FechaInicio == DateTime.MinValue)
            {
                throw new Exception("La fecha de inicio del alquiler es obligatoria");
            }

            if (entidad.FechaFin == DateTime.MinValue)
            {
                throw new Exception("La fecha de fin del alquiler es obligatoria");
            }

            if (entidad.PrecioAlquiler >= 0)
            {
                throw new Exception("El precio del alquiler es obligatorio");
            }
        }

        public Alquileres ConsultarEstadoAlquiler(bool EstadoAlquiler)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var Alquiler = iConexion.Alquileres!.FirstOrDefault(a => a.EstadoAlquiler == EstadoAlquiler);
            var Auditorias = new Auditorias();

            Auditorias.Descripcion = "Se realizo una consulta de estado del Alquiler" + EstadoAlquiler;
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual";
            Auditorias.Accion = "Consulta por Estado";

            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return Alquiler!;
        }

        public List<Alquileres> ConsultarAlquileresPorCliente(int clienteId)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var alquileres = iConexion.Alquileres!.Where(a => a._Clientes != null && a._Clientes.Id == clienteId).ToList();
            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo una consulta de alquileres por cliente con ID: " + clienteId;
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual";
            Auditorias.Accion = "Consulta por Cliente";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return alquileres;
        }

        public bool ExisteCrucedeFechas(int autoId, DateTime FechaInicio, DateTime FechaFin )
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            var existeCruce = iConexion.Alquileres!.Any(a => a._Autos != null && a._Autos.Id == autoId &&
                ((FechaInicio >= a.FechaInicio && FechaInicio <= a.FechaFin) ||
                 (FechaFin >= a.FechaInicio && FechaFin <= a.FechaFin) ||
                 (FechaInicio <= a.FechaInicio && FechaFin >= a.FechaFin)));
            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se verifico el cruce de fechas para el auto con ID: " + autoId;
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual";
            Auditorias.Accion = "Verificacion de Cruce de Fechas";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return existeCruce;

        }

        public decimal CalcularTotalPrecio (decimal PrecioAlquiler, DateTime FechaInicio, DateTime FechaFin)
        {
            int dias = (FechaFin - FechaInicio).Days;

            if(dias <= 0)
            {
                dias = 1;
            }

            return dias * PrecioAlquiler;
        }
     

    }
}
