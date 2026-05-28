using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class MantenimientosNegocio : IMantenimientosNegocio
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

        public List<Mantenimientos> Consultar()
        {
            AbrirConexion();
            var lista = iConexion!.Mantenimientos!.ToList();

            foreach (var mantenimiento in lista)
            {
                mantenimiento.Auto = iConexion.Autos!
                    .FirstOrDefault(a => a.Id == mantenimiento.Autos);
                mantenimiento.Taller = iConexion.Talleres!
                    .FirstOrDefault(t => t.Id == mantenimiento.Talleres);
            }

            RegistrarAuditoria("Se realizó una consulta en Mantenimientos", "Consulta");
            return lista;
        }

        public Mantenimientos Guardar(Mantenimientos entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            BloquearAuto(entidad.Autos!.Value);

            iConexion!.Mantenimientos!.Add(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se registró mantenimiento tipo " + entidad.Tipo + " para el auto ID " + entidad.Autos,
                "Guardado");

            return entidad;
        }

        public Mantenimientos Eliminar(Mantenimientos entidad)
        {
            AbrirConexion();
            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El mantenimiento con ID " + entidad.Id + " no existe en el sistema");
            }

            iConexion!.Mantenimientos!.Remove(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se eliminó el mantenimiento con ID " + entidad.Id,
                "Eliminacion");

            return entidad;
        }

        public Mantenimientos Modificar(Mantenimientos entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El mantenimiento con ID " + entidad.Id + " no existe en el sistema");
            }

            iConexion!.Mantenimientos!.Update(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se modificó el mantenimiento con ID " + entidad.Id,
                "Modificacion");

            return entidad;
        }

        public bool ValidarId(int id)
        {
            if (iConexion == null)
            {
                AbrirConexion();
            }

            return iConexion!.Mantenimientos!.Any(m => m.Id == id);
        }
        public Mantenimientos ConsultarPorId(int id)
        {
            AbrirConexion();

            var mantenimiento = iConexion!.Mantenimientos!
                .Include(m => m.Auto)
                .Include(m => m.Taller)
                .FirstOrDefault(m => m.Id == id);
            if (mantenimiento == null)
            {
                throw new Exception("No se encontró ningún mantenimiento con ID " + id);
            }

            RegistrarAuditoria(
                "Se consultó el mantenimiento con ID: " +id,
                "Consulta por ID");

            return mantenimiento;
        }
        public List<Mantenimientos> ConsultarPorAuto(int autoId)
        {
            AbrirConexion();

            var lista = iConexion!.Mantenimientos!
                .Where(m => m.Auto!= null && m.Auto.Id == autoId)
                .OrderByDescending(m => m.Fecha)
                .ToList();

            RegistrarAuditoria(
                "Se consultó historial de mantenimientos del auto ID: " + autoId,
                "Consulta por Auto");

            return lista;
        }

        public List<Mantenimientos> ConsultarPorTaller(int tallerId)
        {
            AbrirConexion();

            var lista = iConexion!.Mantenimientos!
                .Where(m => m.Taller!= null && m.Taller.Id == tallerId)
                .OrderByDescending(m => m.Fecha)
                .ToList();

            RegistrarAuditoria(
                "Se consultaron mantenimientos del taller ID: " + tallerId,
                "Consulta por Taller");

            return lista;
        }

        public Mantenimientos FinalizarMantenimiento(int mantenimientoId)
        {
            AbrirConexion();

            var mantenimiento = iConexion!.Mantenimientos!.FirstOrDefault(m => m.Id == mantenimientoId);
            if (mantenimiento == null)
            {
                throw new Exception("No se encontró ningún mantenimiento con ID " + mantenimientoId);
            }

            // Reactivar el auto al finalizar el mantenimiento
            if (mantenimiento.Auto!= null)
            {
                ReactivarAuto(mantenimiento.Auto!.Id);
            }

            RegistrarAuditoria(
                "Se finalizó el mantenimiento ID " + mantenimientoId + " — auto reactivado",
                "Finalizacion Mantenimiento");

            return mantenimiento;
        }

        private void BloquearAuto(int autoId)
        {
            var auto = iConexion!.Autos!.FirstOrDefault(a => a.Id == autoId);
            if (auto != null)
            {
                auto.Estado = false;
                iConexion.Autos!.Update(auto);
                iConexion.SaveChanges();

                RegistrarAuditoria(
                    "Auto ID " + autoId + " bloqueado por mantenimiento",
                    "Bloqueo Auto");
            }
        }
        private void ReactivarAuto(int autoId)
        {
            var auto = iConexion!.Autos!.FirstOrDefault(a => a.Id == autoId);
            if (auto != null)
            {
                auto.Estado = true;
                iConexion.Autos!.Update(auto);
                iConexion.SaveChanges();

                RegistrarAuditoria(
                    "Auto ID " + autoId + " reactivado tras finalizar mantenimiento",
                    "Reactivacion Auto");
            }
        }

        public void ValidarDatos(Mantenimientos entidad)
        {
        {
            if (entidad == null)
                throw new Exception("La información del mantenimiento es obligatoria");

            if (entidad.Autos == null || entidad.Autos == 0)
                throw new Exception("El mantenimiento debe estar asociado a un auto");

            if (entidad.Talleres == null || entidad.Talleres == 0)
                throw new Exception("El mantenimiento debe estar asociado a un taller");

            if (entidad.Fecha == DateTime.MinValue)
                throw new Exception("La fecha del mantenimiento es obligatoria");

            if (string.IsNullOrEmpty(entidad.Descripcion))
                throw new Exception("La descripción del mantenimiento es obligatoria");

            if (string.IsNullOrEmpty(entidad.Tipo))
                throw new Exception("El tipo de mantenimiento es obligatorio");

            if (entidad.Costo <= 0)
                throw new Exception("El costo del mantenimiento debe ser mayor a cero");
        }
    }
    }
}