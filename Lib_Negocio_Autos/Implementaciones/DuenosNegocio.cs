using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class DuenosNegocio : IDuenosNegocio
    {
        private IConexion? iConexion;
        public List<Duenos> Consultar()
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var lista = iConexion.Duenos!.ToList();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo una consulta en Duenos";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Consulta";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();

            return lista;
        }

        public Duenos Guardar(Duenos entidad)
        {

            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            ValidarDatos(entidad!);
            iConexion.Duenos!.Add(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo un guardado en Duenos";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Guardado";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public Duenos Eliminar(Duenos entidad)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            iConexion.Duenos!.Remove(entidad!);
            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El registro no existe");
            }
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se elimino un registro en Duenos";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Eliminacion";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public Duenos Modificar(Duenos entidad)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            ValidarDatos(entidad!);
            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El registro no existe");
            }
            iConexion.Duenos!.Update(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se modifico un registro en Duenos";
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
            var Dueno = iConexion.Duenos!.FirstOrDefault(d => d.Id == id);
            return Dueno!= null;
        }
        public void ValidarDatos(Duenos entidad)
        {
            if (entidad == null)
            {
                throw new Exception("La información del Dueño es obligatoria");
            }

            if (string.IsNullOrEmpty(entidad.Nombre))
            {
                throw new Exception("El nombre del Dueño es obligatorio");
            }

            if (string.IsNullOrEmpty(entidad.Apellido))
            {
                throw new Exception("El apellido del Dueño es obligatorio");
            }

            if (string.IsNullOrEmpty(entidad.Cedula))
            {
                throw new Exception("La cédula del Dueño es obligatoria");
            }

            if (entidad.Edad > 18)
            {
                throw new Exception("La edad del Dueño debe ser mayor de edad");
            }

            if (string.IsNullOrEmpty(entidad.Correo))
            {
                throw new Exception("El correo del Dueño es obligatorio");
            }

            if (string.IsNullOrEmpty(entidad.Telefono))
            {
                throw new Exception("El teléfono del Dueño es obligatorio");
            }

            if (entidad.CantidadAutos >= 0)
            {
                throw new Exception("Cantidad de autos debe ser positivo");
            }
        }

        public Duenos ConsultarPorCedula(string Cedula)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var Dueno = iConexion.Duenos!.FirstOrDefault(d => d.Cedula == Cedula);
            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo una consulta en Duenos por cédula";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual";
            Auditorias.Accion = "Consulta por cédula";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return Dueno!;
        }

        public void verificarEstadoDueno(Duenos dueno)
        {
            if (dueno.Estado == false)
            {
                throw new Exception("El dueño no se encuentra activo");
            }
            else 
            {
                throw new Exception("El dueño se encuentra activo");

            }
        }
    }
}
