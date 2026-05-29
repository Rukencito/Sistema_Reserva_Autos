using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class ResenasNegocio : IResenasNegocio
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
        public List<Resenas> Consultar()
        {
            AbrirConexion();
            var lista = iConexion!.Resenas!.ToList();

            foreach (var resena in lista)
            {
                resena.Cliente = iConexion.Clientes!
                    .FirstOrDefault(c => c.Id == resena.Clientes);
            }

            RegistrarAuditoria("Se consultaron las reseñas", "Consulta");
            return lista;
        }

        public Resenas Guardar(Resenas entidad)
        {
            AbrirConexion();
            iConexion!.Resenas!.Add(entidad!);
            iConexion.SaveChanges();
            RegistrarAuditoria("Se guardo un nuevo registro en Reseñas", "Creacion");
            return entidad;
        }

        public Resenas Eliminar(Resenas entidad)
        {
            AbrirConexion();
            iConexion!.Resenas!.Remove(entidad!);
            iConexion.SaveChanges();
            RegistrarAuditoria("Se elimino un registro en Reseñas", "Eliminacion");
            return entidad;
        }

        public Resenas Modificar(Resenas entidad)
        {
            AbrirConexion();
            iConexion!.Resenas!.Update(entidad!);
            iConexion.SaveChanges();
            RegistrarAuditoria("Se modifico un registro en Reseñas", "Modificacion");
            return entidad;
        }

        public bool ValidarId(int id)
        {
            AbrirConexion();

            var resena = iConexion!.Resenas!.FirstOrDefault(r => r.Id == id);
            return resena != null;
        }

        public void ValidarDatos(Resenas entidad)
        {
            if (entidad == null)
                throw new Exception("La información de la reseña es obligatoria");

            if (entidad.Fecha == default)
                throw new Exception("La fecha de la reseña es obligatoria");

            if (entidad.Calificacion < 1 || entidad.Calificacion > 5)
                throw new Exception("La calificación debe estar entre 1 y 5");

            if (string.IsNullOrEmpty(entidad.Comentario))
                throw new Exception("El comentario de la reseña es obligatorio");

            if (string.IsNullOrEmpty(entidad.TipoServicio))
                throw new Exception("El tipo de servicio de la reseña es obligatorio");

            if (entidad.Clientes <= 0)
                throw new Exception("El cliente de la reseña es obligatorio");
        }
    public List<Resenas> ConsultarPorCliente(int idCliente)
        {
            AbrirConexion();

            var lista = iConexion!.Resenas!
                .Where(r => r.Clientes == idCliente)
                .ToList();

            if (lista.Count == 0)
                throw new Exception("No se encontraron reseñas para el cliente");

            RegistrarAuditoria(
                "Se realizo una consulta en Reseñas por el cliente", "Consulta");
            return lista;
        }
    }
    }
