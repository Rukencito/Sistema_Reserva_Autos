using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;
using System.ComponentModel.DataAnnotations;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class EmpleadosNegocio : IEmpleadosNegocio
    {
        private IConexion? iConexion;
        public List<Empleados> Consultar()
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var lista = iConexion.Empleados!.ToList();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo una consulta en Empleados";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Consulta";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();

            return lista;
        }

        public Empleados Guardar(Empleados entidad)
        {

            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            ValidarDatos(entidad!);
            iConexion.Empleados!.Add(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo un guardado en Empleados";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Guardado";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public Empleados Eliminar(Empleados entidad)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El registro no existe");
            }
            iConexion.Empleados!.Remove(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se elimino un registro en Empleados";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Eliminacion";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public Empleados Modificar(Empleados entidad)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            ValidarDatos(entidad!);
            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El registro no existe");
            }
            iConexion.Empleados!.Update(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se modifico un registro en Empleados";
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
            return Empleado!= null;
        }

        public void ValidarDatos(Empleados entidad)
        {
            if (entidad == null)
            {
                throw new Exception("La información del Empleado es obligatoria");
            }

            if (string.IsNullOrEmpty(entidad.Nombre))
            {
                throw new Exception("El nombre del Empleado es obligatorio");
            }

            if (string.IsNullOrEmpty(entidad.Apellido))
            {
                throw new Exception("El apellido del Empleado es obligatorio");
            }

            if (string.IsNullOrEmpty(entidad.Cedula))
            {
                throw new Exception("La cédula del Empleado es obligatoria");
            }

            if (entidad.Edad > 18)
            {
                throw new Exception("La edad del Empleado debe ser mayor de edad");
            }

            if (string.IsNullOrEmpty(entidad.Correo))
            {
                throw new Exception("El correo del Empleado es obligatorio");
            }

            if (string.IsNullOrEmpty(entidad.Telefono))
            {
                throw new Exception("El teléfono del Empleado es obligatorio");
            }

            if (string.IsNullOrEmpty(entidad.Cargo))
            {
                throw new Exception("El cargo del Empleado es obligatorio");
            }
            if (string.IsNullOrEmpty(entidad.Horario))
            {
                throw new Exception("El horario del Empleado es obligatorio");
            }
            if (entidad.Salario >= 0)
            {
                throw new Exception("El Salario es obligatorio");
            }
            if (entidad.Bonificaciones >= 0)
            {
                throw new Exception("Las bonificaciones del Empleado son obligatorias");
            }
        }

        public Empleados ConsultarPorCedula(string Cedula)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            var Empleado = iConexion.Empleados!.FirstOrDefault(d => d.Cedula == Cedula);
            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo una consulta en Empleados por cédula";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual";
            Auditorias.Accion = "Consulta";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return Empleado!;
        }
        
    }
}
