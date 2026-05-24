using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class ContratosNegocio : IContratosNegocio
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

        public List<Contratos> Consultar()
        {
            AbrirConexion();
            var lista = iConexion!.Contratos!.ToList();
            RegistrarAuditoria("Se realizó una consulta en Contratos", "Consulta");
            return lista;
        }

        public Contratos Guardar(Contratos entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            iConexion!.Contratos!.Add(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se guardó un contrato tipo " + entidad.TipoContrato,
                "Guardado");

            return entidad;
        }

        public Contratos Eliminar(Contratos entidad)
        {
            AbrirConexion();
            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El contrato con ID " + entidad.Id + " no existe en el sistema");
            }

            iConexion!.Contratos!.Remove(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se eliminó el contrato con ID " + entidad.Id,
                "Eliminacion");

            return entidad;
        }

        public Contratos Modificar(Contratos entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El contrato con ID " + entidad.Id + " no existe en el sistema");
            }

            iConexion!.Contratos!.Update(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se modificó el contrato con ID " + entidad.Id,
                "Modificacion");

            return entidad;
        }

        public bool ValidarId(int id)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.Contratos!.Any(c => c.Id == id);
        }

        public Contratos ConsultarPorId(int id)
        {
            AbrirConexion();

            var contrato = iConexion!.Contratos!
                .Include(c => c.Alquiler) // ← esta línea es la clave
                .FirstOrDefault(c => c.Id == id);

            if (contrato == null)
                throw new Exception("No se encontro ningun contrato con ID " + id);

            RegistrarAuditoria(
                "Se consulto el contrato con ID: " + id,
                "Consulta por ID");

            return contrato;
        }

        public List<Contratos> ConsultarPorAlquiler(int alquilerId)
        {
            AbrirConexion();

            var lista = iConexion!.Contratos!
                .Where(c => c.Alquiler != null && c.Alquiler.Id == alquilerId)
                .ToList();

            RegistrarAuditoria(
                "Se consultaron contratos del alquiler con ID: " + alquilerId,
                "Consulta por Alquiler");

            return lista;
        }

        public List<Contratos> ConsultarPorTipo(string tipoContrato)
        {
            AbrirConexion();

            var lista = iConexion!.Contratos!
                .Where(c => c.TipoContrato!.ToLower().Contains(tipoContrato.ToLower()))
                .ToList();

            RegistrarAuditoria(
                "Se consultaron contratos por tipo: " + tipoContrato,
                "Consulta por Tipo");

            return lista;
        }

        public List<Contratos> ConsultarVencidos()
        {
            AbrirConexion();

            var vencidos = iConexion!.Contratos!
                .Where(c => c.FechaFin < DateTime.Now)
                .ToList();

            RegistrarAuditoria("Se consultaron contratos vencidos", "Consulta Vencidos");
            return vencidos;
        }

        public List<Contratos> ConsultarActivos()
        {
            AbrirConexion();

            var ahora = DateTime.Now;
            var activos = iConexion!.Contratos!
                .Where(c => c.FechaInicio <= ahora && c.FechaFin >= ahora)
                .ToList();

            RegistrarAuditoria("Se consultaron contratos activos", "Consulta Activos");
            return activos;
        }

        public void ValidarDatos(Contratos entidad)
        {
            if (entidad == null)
                throw new Exception("La información del contrato es obligatoria");

            if (string.IsNullOrEmpty(entidad.TipoContrato))
                throw new Exception("El tipo de contrato es obligatorio");

            if (string.IsNullOrEmpty(entidad.Descripcion))
                throw new Exception("La descripción del contrato es obligatoria");

            if (entidad.FechaInicio == DateTime.MinValue)
                throw new Exception("La fecha de inicio del contrato es obligatoria");

            if (entidad.FechaFin == DateTime.MinValue)
                throw new Exception("La fecha de fin del contrato es obligatoria");

            if (entidad.FechaFin <= entidad.FechaInicio)
                throw new Exception("La fecha de fin debe ser posterior a la fecha de inicio");

            if (entidad.Alquiler == null)
                throw new Exception("El contrato debe estar asociado a un alquiler");
        }
    }
}