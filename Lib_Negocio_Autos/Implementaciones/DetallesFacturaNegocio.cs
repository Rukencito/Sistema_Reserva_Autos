using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class DetallesFacturaNegocio : IDetallesFacturaNegocio
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

        public List<DetallesFactura> Consultar()
        {
            AbrirConexion();
            var lista = iConexion!.DetallesFactura!.ToList();

            foreach (var detalle in lista)
            {
                detalle.Factura = iConexion.Facturas!
                    .FirstOrDefault(f => f.Id == detalle.Facturas);
            }

            RegistrarAuditoria("Se realizó una consulta en DetallesFactura", "Consulta");
            return lista;
        }

        public DetallesFactura Guardar(DetallesFactura entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            iConexion!.DetallesFactura!.Add(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se guardó un detalle de factura tipo" + entidad.TipoFactura,
                "Guardado");

            return entidad;
        }

        public DetallesFactura Eliminar(DetallesFactura entidad)
        {
            AbrirConexion();

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El detalle de factura con ID " + entidad.Id + " no existe en el sistema");
            }

            iConexion!.DetallesFactura!.Remove(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se eliminó el detalle de factura con ID " + entidad.Id,
                "Eliminacion");

            return entidad;
        }

        public DetallesFactura Modificar(DetallesFactura entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (!ValidarId(entidad.Id))
            {
                throw new Exception($"El detalle de factura con ID " + entidad.Id + " no existe en el sistema");
            }

            iConexion!.DetallesFactura!.Update(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se modificó el detalle de factura con ID " + entidad.Id,
                "Modificacion");

            return entidad;
        }

        public bool ValidarId(int id)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.DetallesFactura!.Any(e => e.Id == id);
        }

        public DetallesFactura ConsultarPorId(int id)
        {
            AbrirConexion();

            var entidad = iConexion!.DetallesFactura!
                .Include(e => e.Factura) 
                .FirstOrDefault(e => e.Id == id);

            if (entidad == null)
            {
                throw new Exception("No se encontró ningún detalle de factura con ID " + id);
            }

            RegistrarAuditoria(
                "Se consultó el detalle de factura con ID: " + id,
                "Consulta por ID");

            return entidad;
        }

        public List<DetallesFactura> ConsultarPorFactura(int facturaId)
        {
            AbrirConexion();

            var lista = iConexion!.DetallesFactura!
                .Where(e => e.Factura!= null && e.Factura.Id == facturaId)
                .ToList();

            RegistrarAuditoria(
                "Se consultaron detalles de la factura con ID: " + facturaId,
                "Consulta por Factura");

            return lista;
        }

        public decimal CalcularSubtotalPorFactura(int facturaId)
        {
            AbrirConexion();

            var detalles = iConexion!.DetallesFactura!
                .Where(e => e.Factura!= null && e.Factura.Id == facturaId)
                .ToList();

            if (!detalles.Any())
                throw new Exception("No se encontraron detalles para la factura con ID " + facturaId);

            decimal subtotal = detalles.Sum(e => e.Subtotal ?? 0);

            RegistrarAuditoria(
                "Se calculó el subtotal de la factura con ID: " + facturaId,
                "Calculo Subtotal");

            return subtotal;
        }

        public void ValidarDatos(DetallesFactura entidad)
        {
            if (entidad == null)
                throw new ArgumentException("La información del detalle de factura es obligatoria");

            if (entidad.Subtotal == null || entidad.Subtotal <= 0)
                throw new ArgumentException("El subtotal debe ser un valor positivo");

            if (string.IsNullOrEmpty(entidad.TipoFactura))
                throw new ArgumentException("El tipo de factura no puede estar vacío");

            if (entidad.Facturas == 0)
                throw new ArgumentException("El detalle debe estar asociado a una factura"); ;
        }
    }
}