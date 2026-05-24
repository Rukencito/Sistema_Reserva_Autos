using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class PagosNegocio : IPagosNegocio
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

        public List<Pagos> Consultar()
        {
            AbrirConexion();
            var lista = iConexion!.Pagos!.ToList();
            RegistrarAuditoria("Se realizó una consulta en Pagos", "Consulta");
            return lista;
        }

        public Pagos Guardar(Pagos entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (entidad.Factura!= null && entidad.Factura.Estado == true)
                throw new Exception("La factura ya está marcada como pagada completamente");

            iConexion!.Pagos!.Add(entidad);
            iConexion.SaveChanges();

            if (entidad.Factura != null)
                ActualizarEstadoFactura(entidad.Factura.Id);

            RegistrarAuditoria(
                "Se registró un pago de " + entidad.Monto + " para la factura ID " + entidad.Factura!.Id,
                "Guardado");

            return entidad;
        }

        public Pagos Eliminar(Pagos entidad)
        {
            AbrirConexion();
            if (!ValidarId(entidad.Id))
                throw new Exception("El pago con ID " + entidad.Id + " no existe en el sistema");

            iConexion!.Pagos!.Remove(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se eliminó el pago con ID " + entidad.Id,
                "Eliminacion");

            return entidad;
        }

        public Pagos Modificar(Pagos entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (!ValidarId(entidad.Id))
                throw new Exception("El pago con ID " + entidad.Id + " no existe en el sistema");

            iConexion!.Pagos!.Update(entidad);
            iConexion.SaveChanges();

            if (entidad.Factura != null)
                ActualizarEstadoFactura(entidad.Factura.Id);

            RegistrarAuditoria(
                "Se modificó el pago con ID " + entidad.Id,
                "Modificacion");

            return entidad;
        }
        public bool ValidarId(int id)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.Pagos!.Any(p => p.Id == id);
        }
        public Pagos ConsultarPorId(int id)
        {
            AbrirConexion();

            var pago = iConexion!.Pagos!
                .Include(p => p.Factura) 
                .FirstOrDefault(p => p.Id == id);
            if (pago == null)
            {
                throw new Exception("No se encontró ningún pago con ID " + id);
            }

            RegistrarAuditoria(
                "Se consultó el pago con ID: " + id,
                "Consulta por ID");

            return pago;
        }
        public List<Pagos> ConsultarPorFactura(int facturaId)
        {
            AbrirConexion();

            var pagos = iConexion!.Pagos!
                .Where(p => p.Factura!= null && p.Factura.Id == facturaId)
                .Include(p => p.Factura)
                .OrderByDescending(p => p.FechaPago)
                .ToList();

            RegistrarAuditoria(
                "Se consultaron pagos de la factura ID: " + facturaId,
                "Consulta por Factura");

            return pagos;
        }
        private void ActualizarEstadoFactura(int facturaId)
        {
            var factura = iConexion!.Facturas!.FirstOrDefault(f => f.Id == facturaId);
            if (factura == null) return;

            decimal totalPagado = iConexion.Pagos!
                .Where(p => p.Factura!= null && p.Factura.Id == facturaId)
                .Sum(p => p.Monto);

            // Si el total pagado cubre el total de la factura, marcarla como pagada
            bool facturaCubierta = totalPagado >= (decimal)factura.Total;
            if (factura.Estado != facturaCubierta)
            {
                factura.Estado = facturaCubierta;
                iConexion.Facturas!.Update(factura);
                iConexion.SaveChanges();

                RegistrarAuditoria(
                    "Factura ID " + facturaId + " actualizada — Estado: " + (facturaCubierta ? "Pagada" : "Pendiente"),
                    "Actualizacion Estado Factura");
            }
        }

        public void ValidarDatos(Pagos entidad)
        {
            if (entidad == null)
                throw new Exception("La información del pago es obligatoria");

            if (entidad.Factura == null)
                throw new Exception("El pago debe estar asociado a una factura");

            if (entidad.Monto <= 0)
                throw new Exception("El monto del pago debe ser mayor a cero");

            if (entidad.FechaPago == DateTime.MinValue)
                throw new Exception("La fecha del pago es obligatoria");

            if (string.IsNullOrEmpty(entidad.MetodoPago))
                throw new Exception("El método de pago es obligatorio");
        }
    }
}