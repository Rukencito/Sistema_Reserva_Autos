using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class ParqueaderosNegocio : IParqueaderosNegocio
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
                Usuario = "UsuarioActual", // Reemplaza con el usuario de sesión
                Accion = accion
            });
            iConexion.SaveChanges();
        }
        public List<Parqueaderos> Consultar()
        {
            AbrirConexion();
            var lista = iConexion!.Parqueaderos!.ToList();
            RegistrarAuditoria("Se realizó una consulta en Parqueaderos", "Consulta");
            return lista;
        }

        public Parqueaderos Guardar(Parqueaderos entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            iConexion!.Parqueaderos!.Add(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se guardó el parqueadero '" + entidad.Nombre + "' con capacidad " + entidad.Capacidad,
                "Guardado");

            return entidad;
        }

        public Parqueaderos Eliminar(Parqueaderos entidad)
        {
            AbrirConexion();

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El parqueadero con ID " + entidad.Id + " no existe en el sistema");
            }

            int autosActuales = ContarAutosEnParqueadero(entidad.Id);
            if (autosActuales > 0)
            {
                throw new Exception(
                    "No se puede eliminar el parqueadero — tiene " + autosActuales + " auto(s) asignado(s)");
            }

            iConexion!.Parqueaderos!.Remove(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se eliminó el parqueadero con ID " + entidad.Id,
                "Eliminacion");

            return entidad;
        }

        public Parqueaderos Modificar(Parqueaderos entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El parqueadero con ID " + entidad.Id + " no existe en el sistema");
            }

            int autosActuales = ContarAutosEnParqueadero(entidad.Id);
            if (entidad.Capacidad < autosActuales)
            {
                throw new Exception(
                    "La nueva capacidad (" + entidad.Capacidad + ") no puede ser menor " +
                    "a la cantidad de autos actuales (" + autosActuales + ")");
            }

            iConexion!.Parqueaderos!.Update(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se modificó el parqueadero con ID " + entidad.Id,
                "Modificacion");

            return entidad;
        }

        public bool ValidarId(int id)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.Parqueaderos!.Any(p => p.Id == id);
        }
        public Parqueaderos ConsultarPorId(int id)
        {
            AbrirConexion();

            var parqueadero = iConexion!.Parqueaderos!.FirstOrDefault(p => p.Id == id);
            if (parqueadero == null)
            {
                throw new Exception("No se encontró ningún parqueadero con ID " + id);
            }

            RegistrarAuditoria(
                "Se consultó el parqueadero con ID: " + id,
                "Consulta por ID");

            return parqueadero;
        }

        public int ContarAutosEnParqueadero(int parqueaderoId)
        {
            if (iConexion == null) AbrirConexion();

            return iConexion!.Autos!
                .Count(a => a.Parqueadero!= null && a.Parqueadero.Id == parqueaderoId);
        }

        public int ConsultarEspaciosDisponibles(int parqueaderoId)
        {
            AbrirConexion();

            var parqueadero = iConexion!.Parqueaderos!.FirstOrDefault(p => p.Id == parqueaderoId);
            if (parqueadero == null)
            {
                throw new Exception("No se encontró ningún parqueadero con ID " + parqueaderoId);
            }

            int autosActuales = ContarAutosEnParqueadero(parqueaderoId);
            int disponibles = parqueadero.Capacidad - autosActuales;

            RegistrarAuditoria(
                "Se consultaron espacios disponibles del parqueadero ID " + parqueaderoId + ": " + disponibles,
                "Consulta Espacios Disponibles");

            return disponibles;
        }
        public bool TieneEspacioDisponible(int parqueaderoId)
        {
            AbrirConexion();

            var parqueadero = iConexion!.Parqueaderos!.FirstOrDefault(p => p.Id == parqueaderoId);
            if (parqueadero == null)
            {
                throw new Exception("No se encontró ningún parqueadero con ID " + parqueaderoId);
            }

            int autosActuales = ContarAutosEnParqueadero(parqueaderoId);
            bool hayEspacio = autosActuales < parqueadero.Capacidad;

            RegistrarAuditoria(
                "Se verificó disponibilidad del parqueadero ID " + parqueaderoId + ": " + (hayEspacio ? "Con espacio" : "Lleno"),
                "Verificacion Espacio");

            return hayEspacio;
        }

        public List<Parqueaderos> ConsultarConEspacioDisponible()
        {
            AbrirConexion();

            var parqueaderos = iConexion!.Parqueaderos!.ToList();

            var disponibles = parqueaderos
                .Where(p => ContarAutosEnParqueadero(p.Id) < p.Capacidad)
                .ToList();

            RegistrarAuditoria(
                "Se consultaron parqueaderos con espacio disponible",
                "Consulta Con Espacio");

            return disponibles;
        }

        public List<Autos> ConsultarAutosPorParqueadero(int parqueaderoId)
        {
            AbrirConexion();

            if (!ValidarId(parqueaderoId))
                throw new Exception($"No se encontró ningún parqueadero con ID {parqueaderoId}");

            var autos = iConexion!.Autos!
                .Where(a => a.Parqueadero!= null && a.Parqueadero.Id == parqueaderoId)
                .ToList();

            RegistrarAuditoria(
                $"Se consultaron autos del parqueadero ID: {parqueaderoId}",
                "Consulta Autos por Parqueadero");

            return autos;
        }

        public void ValidarDatos(Parqueaderos entidad)
        {
            if (entidad == null)
                throw new Exception("La información del parqueadero es obligatoria");

            if (string.IsNullOrEmpty(entidad.Nombre))
                throw new Exception("El nombre del parqueadero es obligatorio");

            if (string.IsNullOrEmpty(entidad.Direccion))
                throw new Exception("La dirección del parqueadero es obligatoria");

            if (entidad.Capacidad <= 0)
                throw new Exception("La capacidad del parqueadero debe ser mayor a cero");
        }
    }
}