using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class TalleresNegocio : ITalleresNegocio
    {
        public string UsuarioSesion { get; set; } = "";

        private IConexion? iConexion;
        private void AbrirConexion()
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");
        }

        private void RegistrarAuditoria(string descripcion, string accion)
        {
            iConexion!.Auditorias!.Add(new Auditorias
            {
                Descripcion = descripcion,
                FechaHora = DateTime.Now,
                Usuario = UsuarioSesion,
                Accion = accion
            });
            iConexion.SaveChanges();
        }
        public List<Talleres> Consultar()
        {
           AbrirConexion();
            var lista = iConexion!.Talleres!.ToList();
            RegistrarAuditoria("Se realizo una consulta en Talleres", "Consulta");
            return lista;
        }

        public Talleres Guardar(Talleres entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);
            iConexion!.Talleres!.Add(entidad!);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se guardo un nuevo registro en Talleres", "Creacion");
            return entidad;
        }

        public Talleres Eliminar(Talleres entidad)
        {
           AbrirConexion();
            ValidarDatos(entidad);

            if (!ValidarId(entidad.Id))
                throw new Exception("El Taller con ID " + entidad.Id + " no existe en el sistema");
            iConexion!.Talleres!.Remove(entidad!);
            iConexion.SaveChanges();

           RegistrarAuditoria("Se elimino un registro en Talleres", "Eliminacion");
            return entidad;
        }

        public Talleres Modificar(Talleres entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (!ValidarId(entidad.Id))
                throw new Exception("El Taller con ID " + entidad.Id + " no existe en el sistema");

            iConexion!.Talleres!.Update(entidad!);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se modifico un registro en Talleres", "Modificacion");
            return entidad;
        }
        public bool ValidarId(int id)
        {
            AbrirConexion();

            var taller = iConexion!.Talleres!.FirstOrDefault(t => t.Id == id);
            return taller != null;
        }

        public void ValidarDatos(Talleres entidad)
        {
            if (entidad == null)
                throw new Exception("La información del taller es obligatoria");

            if (string.IsNullOrEmpty(entidad.Nombre))
                throw new Exception("El nombre del taller es obligatorio");

            if (string.IsNullOrEmpty(entidad.Direccion))
                throw new Exception("La dirección del taller es obligatoria");

            if (string.IsNullOrEmpty(entidad.Telefono))
                throw new Exception("El teléfono del taller es obligatorio");

            if (entidad.Capacidad <= 0)
                throw new Exception("La capacidad del taller es obligatoria");
        }

    }
}
