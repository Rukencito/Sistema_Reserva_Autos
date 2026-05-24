using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class ResenasNegocio : IResenasNegocio
    {
        private IConexion? iConexion;
        public List<Resenas> Consultar()
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var lista = iConexion.Resenas!.ToList();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo una consulta en Reseñas";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Consulta";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();

            return lista;
        }

        public Resenas Guardar(Resenas entidad)
        {

            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            iConexion.Resenas!.Add(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo un guardado en Reseñas";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Guardado";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public Resenas Eliminar(Resenas entidad)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            iConexion.Resenas!.Remove(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se elimino un registro en Reseñas";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Eliminacion";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public Resenas Modificar(Resenas entidad)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            iConexion.Resenas!.Update(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se modifico un registro en Reseñas";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Modificacion";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }
    }
}
