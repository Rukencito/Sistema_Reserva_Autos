using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class FacturasNegocio : IFacturasNegocio
    {
        public string UsuarioSesion { get; set; } = "";

        private IConexion? iConexion;

        private const decimal IVA = 0.19m;

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

        public List<Facturas> Consultar()
        {
            AbrirConexion();
            var lista = iConexion!.Facturas!.ToList();
            RegistrarAuditoria("Se realizó una consulta en Facturas", "Consulta");
            return lista;
        }

        public Facturas Guardar(Facturas entidad)
        {
            AbrirConexion();

            ValidarDatos(entidad);

            CalcularTotal(entidad);

            iConexion!.Facturas!.Add(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se guardó un registro en Facturas", "Guardado");
            return entidad;
        }

        public Facturas Eliminar(Facturas entidad)
        {
            AbrirConexion();
            if (!ValidarId(entidad.Id))
            {
                throw new Exception("La factura con ID " + entidad.Id + "no existe en el sistema");
            }

            iConexion!.Facturas!.Remove(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se eliminó la factura con ID " + entidad.Id, "Eliminacion");
            return entidad;
        }

        public Facturas Modificar(Facturas entidad)
        {
            AbrirConexion();

            ValidarDatos(entidad);

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("La factura con ID " + entidad.Id + " no existe en el sistema");
            }

            CalcularTotal(entidad);

            iConexion!.Facturas!.Update(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se modificó la factura con ID " + entidad.Id, "Modificacion");
            return entidad;
        }

        public bool ValidarId(int id)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.Facturas!.Any(f => f.Id == id);
        }

        public decimal CalcularTotal(Facturas factura)
        {
            decimal subtotal = 0;

            if (factura.DetalleFactura != null && factura.DetalleFactura.Any())
            {
                subtotal = factura.DetalleFactura.Sum(x => x.Subtotal ?? 0);
            }

            decimal iva = subtotal * (decimal)IVA;
            decimal total = subtotal + iva;

            factura.Total = total;

            if (iConexion != null && factura.Id > 0)
            {
                iConexion.Facturas!.Update(factura);
                iConexion.SaveChanges();
            }

            return factura.Total;
        }

        public List<Facturas> ConsultarPorCliente(int clienteId)
        {
            AbrirConexion();

            var facturas = iConexion!.Facturas!
                .Where(f => f.Cliente!= null && f.Cliente.Id == clienteId)
                .ToList();

            RegistrarAuditoria(
                "Se consultaron facturas del cliente con ID: " + clienteId,
                "Consulta por Cliente");

            return facturas;
        }

        public Facturas ConsultarPorId(int id)
        {
            AbrirConexion();

            var factura = iConexion!.Facturas!
                .Include(f => f.Cliente)
                .FirstOrDefault(f => f.Id == id);

            if (factura == null)
            {
                throw new Exception("No se encontró ninguna factura con ID {id}");
            }

            RegistrarAuditoria(
                "Se consultó la factura con ID: " + id,
                "Consulta por ID");

            return factura;
        }

        public List<Facturas> ConsultarPendientes()
        {
            AbrirConexion();

            var pendientes = iConexion!.Facturas!
                .Where(f => f.Estado == false)
                .ToList();

            RegistrarAuditoria("Se consultaron facturas pendientes de pago", "Consulta Pendientes");
            return pendientes;
        }

        public Facturas MarcarComoPagada(int facturaId)
        {
            AbrirConexion();

            var factura = iConexion!.Facturas!.FirstOrDefault(f => f.Id == facturaId);
            if (factura == null)
            {
                throw new Exception("No se encontró ninguna factura con ID " + facturaId);
            }

            if (factura.Estado == true)
            {
                throw new Exception("La factura con ID " + facturaId + " ya está marcada como pagada");
            }

            factura.Estado = true;
            iConexion.Facturas!.Update(factura);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se marcó como pagada la factura con ID " + facturaId, "Pago de Factura");
            return factura;
        }

        public void ValidarDatos(Facturas entidad)
        {
            if (entidad == null)
                throw new Exception("La información de la factura es obligatoria");

            if (entidad.Cliente == null)
                throw new Exception("La factura debe estar asociada a un cliente");

            if (entidad.Total < 0)
                throw new Exception("El total de la factura no puede ser negativo");

            if (entidad.FechaEmision == DateTime.MinValue)
                throw new Exception("La fecha de emisión de la factura es obligatoria");
        }
    }
}