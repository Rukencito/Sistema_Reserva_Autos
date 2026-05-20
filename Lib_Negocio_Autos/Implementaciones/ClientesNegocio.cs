using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class ClientesNegocio : IClientesNegocio
    {
        private IConexion? iConexion;
        public List<Clientes> Consultar()
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var lista = iConexion.Clientes!.ToList();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo una consulta en Clientes";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Consulta";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();

            return lista;
        }

        public Clientes Guardar(Clientes entidad)
        {

            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            ValidarDatos(entidad);

            iConexion.Clientes!.Add(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo un guardado en Clientes";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Guardado";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public Clientes Eliminar(Clientes entidad)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            if (ValidarCedula(entidad.Cedula!))
            {
                throw new Exception("El cliente con cedula: " + entidad.Cedula + " no existe en el sistema");
            }

            iConexion.Clientes!.Remove(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se elimino un registro en Clientes";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Eliminacion";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public Clientes Modificar(Clientes entidad)
        {
           
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            ValidarDatos(entidad);
            if (!ValidarCedula(entidad.Cedula!))
            {
                throw new Exception("El cliente con cedula: " + entidad.Cedula + " no existe en el sistema");
            }

            iConexion.Clientes!.Update(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se modifico un registro en Clientes";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Modificacion";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public Clientes ConsultarPorCedula(string cedula)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var auto = iConexion.Clientes!.FirstOrDefault(a => a.Cedula == cedula);
            var Auditorias = new Auditorias();

            Auditorias.Descripcion = "Se realizo una consulta en Clintes por cédula: " + cedula;
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Consulta por Cédula";

            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return auto!;

        }

        public bool ValidarCedula(string cedula)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            var cliente = iConexion.Clientes!.FirstOrDefault(a => a.Cedula == cedula);
            return cliente != null;
        }
        public void ValidarDatos(Clientes entidad)
        {
            if (string.IsNullOrEmpty(entidad.Nombre))
            {
                throw new Exception("El nombre del cliente es obligatorio.");
            }
            if (string.IsNullOrEmpty(entidad.Apellido))
            {
                throw new Exception("El apellido del cliente es obligatorio.");
            }
            if (string.IsNullOrEmpty(entidad.Cedula)) 
            {
                throw new Exception("La cédula del cliente es obligatoria.");
            }
            if (entidad.Edad > 18)
            {
                throw new Exception("El cliente debe ser mayor de edad.");
            }
            if (string.IsNullOrEmpty(entidad.Correo))
            {
                throw new Exception("El correo del cliente es obligatorio.");
            }
            if (string.IsNullOrEmpty(entidad.LicenciaConduccion.ToString()))
            {
                throw new Exception("La licencia de conducción del cliente es obligatoria.");
            }
        }
    }
}
