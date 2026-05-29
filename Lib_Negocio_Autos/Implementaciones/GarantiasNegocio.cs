using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class GarantiasNegocio : IGarantiasNegocio
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

        public List<Garantias> Consultar()
        {
            AbrirConexion();
            var lista = iConexion!.Garantias!.ToList();
            RegistrarAuditoria("Se realizó una consulta en Garantias", "Consulta");
            return lista;
        }

        public Garantias Guardar(Garantias entidad)
        {
            AbrirConexion();

            ValidarDatos(entidad);

            iConexion!.Garantias!.Add(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se guardó una garantía para el auto ID " + entidad.Auto!.Id,
                "Guardado");

            return entidad;
        }

        public Garantias Eliminar(Garantias entidad)
        {
            AbrirConexion();

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("La garantía con ID " + entidad.Id + " no existe en el sistema");
            }

            iConexion!.Garantias!.Remove(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se eliminó la garantía con ID " + entidad.Id,
                "Eliminacion");

            return entidad;
        }

        public Garantias Modificar(Garantias entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("La garantía con ID " + entidad.Id + " no existe en el sistema");
            }

            iConexion!.Garantias!.Update(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se modificó la garantía con ID " + entidad.Id,
                "Modificacion");

            return entidad;
        }

        public bool ValidarId(int id)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.Garantias!.Any(g => g.Id == id);
        }

        public Garantias ConsultarPorId(int id)
        {
            AbrirConexion();

            var garantia = iConexion!.Garantias!
                .Include(g => g.Auto) 
                .FirstOrDefault(g => g.Id == id);
            if (garantia == null)
            {
                throw new Exception("No se encontró ninguna garantía con ID " + id);
            }

            RegistrarAuditoria(
                "Se consultó la garantía con ID: " + id,
                "Consulta por ID");

            return garantia;
        }

        public List<Garantias> ConsultarPorAuto(int autoId)
        {
            AbrirConexion();

            var lista = iConexion!.Garantias!
                .Where(g => g.Auto!= null && g.Auto.Id == autoId)
                .ToList();

            RegistrarAuditoria(
                $"Se consultaron garantías del auto con ID: {autoId}",
                "Consulta por Auto");

            return lista;
        }
        public bool TieneGarantiaVigente(int autoId)
        {
            AbrirConexion();

            var ahora = DateTime.Now;
            bool vigente = iConexion!.Garantias!.Any(g =>
                g.Auto!= null &&
                g.Auto.Id == autoId &&
                g.FechaInicio <= ahora &&
                g.FechaFin >= ahora);

            RegistrarAuditoria(
                "Se verificó garantía vigente del auto ID: " + autoId,
                "Verificacion Garantia Vigente");

            return vigente;
        }

        public void ValidarDatos(Garantias entidad)
        {
            if (entidad == null)
                throw new Exception("La información de la garantía es obligatoria");

            if (entidad.Auto == null)
                throw new Exception("La garantía debe estar asociada a un auto");

            if (entidad.FechaInicio == DateTime.MinValue)
                throw new Exception("La fecha de inicio de la garantía es obligatoria");

            if (entidad.FechaFin == DateTime.MinValue)
                throw new Exception("La fecha de fin de la garantía es obligatoria");

            if (entidad.FechaFin <= entidad.FechaInicio)
                throw new Exception("La fecha de fin debe ser posterior a la fecha de inicio");
        }
    }
}