using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class ReservasNegocio : IReservasNegocio
    {
        private IConexion? iConexion;
        public List<Reservas> Consultar()
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var lista = iConexion.Reservas!.ToList();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo una consulta en Reservas";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Consulta";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();

            return lista;
        }

        public Reservas Guardar(Reservas entidad)
        {

            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            ValidarDatos(entidad);
            iConexion.Reservas!.Add(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo un guardado en Reservas";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Guardado";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public Reservas Eliminar(Reservas entidad)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El registro no existe");
            }
            iConexion.Reservas!.Remove(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se elimino un registro en Reservas";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Eliminacion";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public Reservas Modificar(Reservas entidad)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El registro no existe");
            }
            iConexion.Reservas!.Update(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se modifico un registro en Reservas";
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
            var Empleado = iConexion.Empleados!.FirstOrDefault(e => e.Id == id);
            return Empleado != null;
        }

        public bool ValidarReservaDuplicada(int autoId, int clienteId, DateTime fechaVencimiento)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

          
            var existeDuplicado = iConexion.Reservas!.Any(r =>
                r._Autos != null && r._Autos.Id == autoId &&
                r._Clientes != null && r._Clientes.Id == clienteId &&
                r.FechaVencimiento == fechaVencimiento);

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se verifico reserva duplicada para el auto ID: " + autoId +
                                     " y cliente ID: " + clienteId;
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual";
            Auditorias.Accion = "Validacion de Reserva Duplicada";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();

            return existeDuplicado;
        }

        public void ValidarDatos(Reservas entidad)
        {
            if (entidad._Autos == null || entidad._Clientes == null)
            {
                throw new Exception("El auto y el cliente son obligatorios para una reserva.");
            }
            if (entidad.FechaVencimiento <= DateTime.Now)
            {
                throw new Exception("La fecha de vencimiento debe ser futura.");
            }
        }

    }
}
