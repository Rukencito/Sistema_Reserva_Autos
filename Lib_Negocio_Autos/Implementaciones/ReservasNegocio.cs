using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class ReservasNegocio : IReservasNegocio
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

        public List<Reservas> Consultar()
        {
            AbrirConexion();
            var lista = iConexion!.Reservas!.ToList();
            RegistrarAuditoria("Se realizó una consulta en Reservas", "Consulta");
            return lista;
        }

        public Reservas Guardar(Reservas entidad)
        {
            AbrirConexion();

            ValidarDatos(entidad);

            if (ValidarReservaDuplicada(entidad.Auto!.Id, entidad.Cliente!.Id, entidad.FechaVencimiento))
            {
                throw new Exception("Ya existe una reserva activa para ese auto y cliente");
            }

            iConexion!.Reservas!.Add(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se guardó una reserva para el auto ID " + entidad.Auto.Id +" y cliente ID " + entidad.Cliente.Id,
                "Guardado");

            return entidad;
        }

        public Reservas Eliminar(Reservas entidad)
        {
            AbrirConexion();

            if (!ValidarId(entidad.Id))
                throw new Exception("La reserva con ID " + entidad.Id + " no existe en el sistema");

            iConexion!.Reservas!.Remove(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se eliminó la reserva con ID " + entidad.Id, "Eliminacion");
            return entidad;
        }

        public Reservas Modificar(Reservas entidad)
        {
            AbrirConexion();

            ValidarDatos(entidad);

            if (!ValidarId(entidad.Id))
                throw new Exception("La reserva con ID " + entidad.Id + " no existe en el sistema");

            iConexion!.Reservas!.Update(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se modificó la reserva con ID " + entidad.Id, "Modificacion");
            return entidad;
        }

      
        public bool ValidarId(int id)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.Reservas!.Any(r => r.Id == id);
        }

        public bool ValidarReservaDuplicada(int autoId, int clienteId, DateTime fechaVencimiento)
        {
            AbrirConexion();

            var existeDuplicado = iConexion!.Reservas!.Any(r =>
                r.Autos != null && r.Auto!.Id == autoId &&
                r.Clientes != null && r.Cliente!.Id == clienteId &&
                r.FechaVencimiento >= DateTime.Now &&
                (r.EstadoReserva == "Pendiente" || r.EstadoReserva == "Confirmada"));

            RegistrarAuditoria(
                "Se verificó reserva duplicada — auto ID: " + autoId + ", cliente ID: " + clienteId,
                "Validacion de Reserva Duplicada");

            return existeDuplicado;
        }

        public Reservas CambiarEstado(int reservaId, string nuevoEstado)
        {
            AbrirConexion();

            var estadosValidos = new[] { "Pendiente", "Confirmada", "Cancelada" };
            if (!estadosValidos.Contains(nuevoEstado))
                throw new Exception("Estado inválido. Los estados permitidos son: " + string.Join(", ", estadosValidos));

            var reserva = iConexion!.Reservas!.FirstOrDefault(r => r.Id == reservaId);
            if (reserva == null)
                throw new Exception("No se encontró la reserva con ID " + reservaId);

            // No permitir reactivar una reserva ya cancelada
            if (reserva.EstadoReserva == "Cancelada" && nuevoEstado != "Cancelada")
                throw new Exception("No se puede reactivar una reserva cancelada");

            reserva.EstadoReserva = nuevoEstado;
            iConexion.Reservas!.Update(reserva);

            RegistrarAuditoria(
                "Se cambió el estado de la reserva ID " + reservaId + " a " + nuevoEstado,
                "Cambio de Estado");

            return reserva;
        }

        public List<Reservas> ConsultarPorCliente(int clienteId)
        {
            AbrirConexion();

            var reservas = iConexion!.Reservas!
                .Where(r => r.Cliente!= null && r.Cliente.Id == clienteId)
                .ToList();

            RegistrarAuditoria(
                "Se consultaron reservas del cliente con ID: " + clienteId,
                "Consulta por Cliente");

            return reservas;
        }

        public void ValidarDatos(Reservas entidad)
        {
            if (entidad == null)
                throw new Exception("La información de la reserva es obligatoria");

            if (entidad.Auto == null)
                throw new Exception("El auto es obligatorio para una reserva");

            if (entidad.Cliente == null)
                throw new Exception("El cliente es obligatorio para una reserva");

            if (entidad.FechaVencimiento <= DateTime.Now)
                throw new Exception("La fecha de vencimiento debe ser una fecha futura");

            if (entidad.Anticipo < 0)
                throw new Exception("El anticipo no puede ser un valor negativo");

            if (string.IsNullOrEmpty(entidad.EstadoReserva))
                throw new Exception("El estado de la reserva es obligatorio");

            if (entidad.FechaReserva == DateTime.MinValue)
                throw new Exception("La fecha de la reserva es obligatoria");
        }
    }
}