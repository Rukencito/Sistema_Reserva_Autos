using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class SucursalesNegocio : ISucursalesNegocio
    {
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
                Usuario = "UsuarioActual",
                Accion = accion
            });
            iConexion.SaveChanges();
        }

        public List<Sucursales> Consultar()
        {
            AbrirConexion();

            var lista = iConexion!.Sucursales!.ToList();

           RegistrarAuditoria("Se realizo una consulta en Sucursales", "Consulta");
            return lista;
        }

        public Sucursales Guardar(Sucursales entidad)
        {

            AbrirConexion();

            iConexion!.Sucursales!.Add(entidad!);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se guardo un nuevo registro en Sucursales", "Creacion");
            return entidad;
        }

        public Sucursales Eliminar(Sucursales entidad)
        {
            AbrirConexion();
            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El registro no existe");
            }
            iConexion!.Sucursales!.Remove(entidad!);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se elimino un registro en Sucursales", "Eliminacion");
            return entidad;
        }

        public Sucursales Modificar(Sucursales entidad)
        {
            AbrirConexion();
            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El registro no existe");
            }
            iConexion!.Sucursales!.Update(entidad!);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se modifico un registro en Sucursales", "Modificacion");
            return entidad;
        }
        public bool ValidarId(int id)
        {
            AbrirConexion();

            var sucursal = iConexion!.Sucursales!.FirstOrDefault(s => s.Id == id);
            return sucursal != null;
        }

        public List<Sucursales> ConsultarPorCiudad(string ciudad)
        {
            AbrirConexion();

            var lista = iConexion!.Sucursales!
                .Where(s => s.Ciudad!.ToLower() == ciudad.ToLower())
                .ToList();

            RegistrarAuditoria(
                $"Se realizo una consulta en Sucursales por ciudad: {ciudad}",
                "Consulta"
            );
            return lista;
        }
    }
}
