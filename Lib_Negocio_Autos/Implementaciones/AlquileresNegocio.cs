using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class AlquileresNegocio : IAlquileresNegocio
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

        public List<Alquileres> Consultar()
        {
            AbrirConexion();
            var lista = iConexion!.Alquileres!.ToList();
            RegistrarAuditoria("Se realizó una consulta en Alquileres", "Consulta");
            return lista;
        }

        public Alquileres Guardar(Alquileres entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (entidad.Auto != null && ExisteCruceDeFechas(entidad.Auto.Id, entidad.FechaInicio, entidad.FechaFin))
                throw new Exception("El auto ya tiene un alquiler activo en ese rango de fechas");

            iConexion!.Alquileres!.Add(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se guardó un registro en Alquileres", "Guardado");
            return entidad;
        }

        public Alquileres Eliminar(Alquileres entidad)
        {
            AbrirConexion();

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El alquiler con ID " + entidad.Id + " no existe en el sistema");
            }

            iConexion!.Alquileres!.Remove(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se eliminó el alquiler con ID " + entidad.Id, "Eliminacion");
            return entidad;
        }

        public Alquileres Modificar(Alquileres entidad)
        {
            AbrirConexion();

            ValidarDatos(entidad);

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El alquiler con ID " + entidad.Id + " no existe en el sistema");
            }

            iConexion!.Alquileres!.Update(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se modificó el alquiler con ID " + entidad.Id, "Modificacion");
            return entidad;
        }

        public bool ValidarId(int id)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.Alquileres!.Any(a => a.Id == id);
        }

        public List<Alquileres> ConsultarEstadoAlquiler(bool estadoAlquiler)
        {
            AbrirConexion();
            var alquileres = iConexion!.Alquileres!
                .Where(a => a.EstadoAlquiler == estadoAlquiler)
                .ToList();

            RegistrarAuditoria(
                "Se consultaron alquileres por estado: " + estadoAlquiler,
                "Consulta por Estado");

            return alquileres;
        }

        public List<Alquileres> ConsultarAlquileresPorCliente(int clienteId)
        {
            AbrirConexion();

            var alquileres = iConexion!.Alquileres!
                .Where(a => a.Cliente != null && a.Cliente.Id == clienteId)
                .ToList();

            RegistrarAuditoria(
                "Se consultaron alquileres del cliente con ID: " + clienteId,
                "Consulta por Cliente");

            return alquileres;
        }

        public bool ExisteCruceDeFechas(int autoId, DateTime fechaInicio, DateTime fechaFin)
        {
            AbrirConexion();

            var existeCruce = iConexion!.Alquileres!.Any(a =>
                a.Auto != null &&
                a.Auto.Id == autoId &&
                a.EstadoAlquiler == true &&
                ((fechaInicio >= a.FechaInicio && fechaInicio < a.FechaFin) ||
                 (fechaFin > a.FechaInicio && fechaFin <= a.FechaFin) ||
                 (fechaInicio <= a.FechaInicio && fechaFin >= a.FechaFin)));

            RegistrarAuditoria(
                "Se verificó cruce de fechas para el auto con ID: " + autoId,
                "Verificacion de Cruce de Fechas");

            return existeCruce;
        }

        public decimal CalcularTotalPrecio(decimal precioAlquiler, DateTime fechaInicio, DateTime fechaFin)
        {
            int dias = (fechaFin - fechaInicio).Days;

            if (dias <= 0) dias = 1;

            return dias * precioAlquiler;
        }

        public void ValidarDatos(Alquileres entidad)
        {
            if (entidad == null)
                throw new Exception("La información del alquiler es obligatoria");

            if (entidad.FechaInicio == DateTime.MinValue)
                throw new Exception("La fecha de inicio del alquiler es obligatoria");

            if (entidad.FechaFin == DateTime.MinValue)
                throw new Exception("La fecha de fin del alquiler es obligatoria");

            if (entidad.FechaFin <= entidad.FechaInicio)
                throw new Exception("La fecha de fin debe ser posterior a la fecha de inicio");

            if (entidad.PrecioAlquiler <= 0)
                throw new Exception("El precio del alquiler debe ser mayor a cero");
        }
    }
}