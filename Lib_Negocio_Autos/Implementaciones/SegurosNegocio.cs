using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class SegurosNegocio : ISegurosNegocio
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

        public List<Seguros> Consultar()
        {
            AbrirConexion();
            var lista = iConexion!.Seguros!.ToList();

            foreach (var seguro in lista)
            {
                seguro.Auto = iConexion.Autos!
                    .FirstOrDefault(a => a.Id == seguro.Autos);
            }

            RegistrarAuditoria("Se consulto la lista de Seguros", "Consulta");
            return lista;
        }

        public Seguros Guardar(Seguros entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            iConexion!.Seguros!.Add(entidad!);
            iConexion.SaveChanges();
            RegistrarAuditoria("Se guardo un nuevo registro en Seguros", "Creacion");
            return entidad;
        }

        public Seguros Eliminar(Seguros entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (!ValidarId(entidad.Id))
                throw new Exception("El seguro con ID " + entidad.Id + " no existe en el sistema");
            iConexion!.Seguros!.Remove(entidad!);
            iConexion.SaveChanges();
            RegistrarAuditoria("Se elimino un registro en Seguros", "Eliminacion");
            return entidad;
        }

        public Seguros Modificar(Seguros entidad)
        {
          AbrirConexion();
            ValidarDatos(entidad);

            if (!ValidarId(entidad.Id))
                throw new Exception("El seguro con ID " + entidad.Id + " no existe en el sistema");
            iConexion!.Seguros!.Update(entidad!);
            iConexion.SaveChanges();
            RegistrarAuditoria("Se modifico un registro en Seguros", "Modificacion");
            return entidad;
        }
        public bool ValidarId(int id)
        {
            AbrirConexion();

            var seguro = iConexion!.Seguros!.FirstOrDefault(s => s.Id == id);
            return seguro != null;
        }

        public Seguros ConsultarPorId(int id)
        {
            AbrirConexion();

            var seguro = iConexion!.Seguros!.FirstOrDefault(s => s.Id == id);

            if (seguro == null)
                throw new Exception("No se encontró un seguro");

            RegistrarAuditoria("Se realizo una consulta en Seguros", "Consulta");
            return seguro;
        }
        public void ValidarDatos(Seguros entidad)
        {
            if (entidad == null)
                throw new Exception("La información del seguro es obligatoria");

            if (string.IsNullOrEmpty(entidad.Tipo))
                throw new Exception("El tipo de seguro es obligatorio");

            if (string.IsNullOrEmpty(entidad.Cobertura))
                throw new Exception("La cobertura del seguro es obligatoria");

            if (string.IsNullOrEmpty(entidad.Aseguradora))
                throw new Exception("La aseguradora del seguro es obligatoria");
        }
    }
}
