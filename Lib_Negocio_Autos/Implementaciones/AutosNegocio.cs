using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class AutosNegocio : IAutosNegocio
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

        public List<Autos> Consultar()
        {
            AbrirConexion();
            var lista = iConexion!.Autos!.ToList();
            RegistrarAuditoria("Se realizó una consulta en Autos", "Consulta");
            return lista;
        }

        public Autos Guardar(Autos entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            iConexion!.Autos!.Add(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se guardó el auto con placa {entidad.Placa}", "Guardado");
            return entidad;
        }

        public Autos Eliminar(Autos entidad)
        {
            AbrirConexion();

            if (!ValidarPlaca(entidad.Placa!))
            {
                throw new Exception("El auto con la placa " + entidad.Placa + " no existe en el sistema");
            }

            iConexion!.Autos!.Remove(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se eliminó el auto con placa " + entidad.Placa + "", "Eliminacion");
            return entidad;
        }

        public Autos Modificar(Autos entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (!ValidarPlaca(entidad.Placa!))
            {
                throw new Exception("El auto con la placa " + entidad.Placa + " no existe en el sistema");
            }

            iConexion!.Autos!.Update(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se modificó el auto con placa " + entidad.Placa, "Modificacion");
            return entidad;
        }

        public bool ValidarPlaca(string placa)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.Autos!.Any(a => a.Placa == placa);
        }

        public Autos ConsultarPorPlaca(string placa)
        {
            AbrirConexion();

            var auto = iConexion!.Autos!.FirstOrDefault(a => a.Placa == placa);
            if (auto == null)
            {
                throw new Exception("No se encontró ningún auto con la placa " + placa);
            }

            RegistrarAuditoria("Se consultó auto por placa: " + placa, "Consulta por Placa");
            return auto;
        }

        public List<Autos> ConsultarPorMarca(string marca)
        {
            AbrirConexion();

            var autos = iConexion!.Autos!
                .Where(a => a.Marca!.ToLower().Contains(marca.ToLower()))
                .ToList();

            RegistrarAuditoria("Se consultó autos por marca: " + marca, "Consulta por Marca");
            return autos;
        }

        public List<Autos> ConsultarPorModelo(string modelo)
        {
            AbrirConexion();

            var autos = iConexion!.Autos!
                .Where(a => a.Modelo!.ToLower().Contains(modelo.ToLower()))
                .ToList();

            RegistrarAuditoria("Se consultó autos por modelo: " + modelo, "Consulta por Modelo");
            return autos;
        }

        public List<Autos> ConsultarDisponibles()
        {
            AbrirConexion();
            var ahora = DateTime.Now;

            // IDs de autos ocupados por alquiler activo ahora mismo
            var idsEnAlquiler = iConexion!.Alquileres!
                .Where(a => a.EstadoAlquiler == true
                         && a.FechaInicio <= ahora
                         && a.FechaFin >= ahora)
                .Select(a => a.Auto!.Id)
                .ToList();

            // IDs de autos con reserva vigente (Pendiente o Confirmada)
            var idsEnReserva = iConexion.Reservas!
                .Where(r => (r.EstadoReserva == "Pendiente" || r.EstadoReserva == "Confirmada")
                         && r.FechaVencimiento >= ahora)
                .Select(r => r.Auto!.Id)
                .ToList();

            var disponibles = iConexion.Autos!
                .Where(a => a.Estado == true
                         && !idsEnAlquiler.Contains(a.Id)
                         && !idsEnReserva.Contains(a.Id))
                .ToList();

            RegistrarAuditoria("Se consultaron autos disponibles", "Consulta de Autos Disponibles");
            return disponibles;
        }

        public bool VerificarDisponibilidad(string placa)
        {
            AbrirConexion();

            var auto = iConexion!.Autos!.FirstOrDefault(a => a.Placa == placa);
            if (auto == null)
                throw new Exception("El auto con la placa " + placa + " no existe.");

            var ahora = DateTime.Now;

            bool enAlquiler = iConexion.Alquileres!.Any(a =>
                a.Auto!.Id == auto.Id &&
                a.EstadoAlquiler == true &&
                a.FechaInicio <= ahora &&
                a.FechaFin >= ahora);

            bool enReserva = iConexion.Reservas!.Any(r =>
                r.Auto!.Id == auto.Id &&
                (r.EstadoReserva == "Pendiente" || r.EstadoReserva == "Confirmada") &&
                r.FechaVencimiento >= ahora);

            RegistrarAuditoria(
                "Se verificó disponibilidad del auto con placa: " + placa,
                "Verificacion de Disponibilidad");

            return auto.Estado && !enAlquiler && !enReserva;
        }


        public bool CambiarEstado(string placa, bool nuevoEstado)
        {
            AbrirConexion();

            var auto = iConexion!.Autos!.FirstOrDefault(a => a.Placa == placa);
            if (auto == null)
            { 
            throw new Exception("El auto con la placa " + placa + " no existe.");
            }

            auto.Estado = nuevoEstado;
            iConexion.Autos!.Update(auto);

            RegistrarAuditoria(
                "Se cambió el estado del auto con placa " + placa + " a " + nuevoEstado,
                "Cambio de Estado");

            iConexion.SaveChanges();
            return auto.Estado;
        }

        public void ValidarDatos(Autos entidad)
        {
            if (entidad == null)
                throw new Exception("La información del auto es obligatoria");

            if (string.IsNullOrEmpty(entidad.Placa))
                throw new Exception("La placa del auto es obligatoria");

            if (string.IsNullOrEmpty(entidad.Marca))
                throw new Exception("La marca del auto es obligatoria");

            if (entidad.Año <= 1885 || entidad.Año > DateTime.Now.Year + 1)
                throw new Exception("El año del auto debe estar entre 1886 y {DateTime.Now.Year + 1}");

            if (string.IsNullOrEmpty(entidad.Modelo))
                throw new Exception("El modelo del auto es obligatorio");

            if (string.IsNullOrEmpty(entidad.Color))
                throw new Exception("El color del auto es obligatorio");
        }
    }
}