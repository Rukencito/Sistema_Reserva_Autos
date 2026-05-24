using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class InventariosNegocio : IInventariosNegocio
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
        public List<Inventarios> Consultar()
        {
            AbrirConexion();
            var lista = iConexion!.Inventarios!.ToList();
            RegistrarAuditoria("Se realizó una consulta en Inventarios", "Consulta");
            return lista;
        }

        public Inventarios Guardar(Inventarios entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            entidad.FechaActualizacion = DateTime.Now;

            iConexion!.Inventarios!.Add(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se guardó un inventario en ubicación: " + entidad.Ubicacion,
                "Guardado");

            return entidad;
        }

        public Inventarios Eliminar(Inventarios entidad)
        {
            AbrirConexion();
            if (!ValidarId(entidad.Id))
                throw new Exception("El inventario con ID " + entidad.Id + " no existe en el sistema");

            iConexion!.Inventarios!.Remove(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se eliminó el inventario con ID " + entidad.Id,
                "Eliminacion");

            return entidad;
        }

        public Inventarios Modificar(Inventarios entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (!ValidarId(entidad.Id))
                throw new Exception("El inventario con ID " + entidad.Id + " no existe en el sistema");

            entidad.FechaActualizacion = DateTime.Now;

            iConexion!.Inventarios!.Update(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se modificó el inventario con ID " + entidad.Id,
                "Modificacion");

            return entidad;
        }
        public bool ValidarId(int id)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.Inventarios!.Any(i => i.Id == id);
        }
        public Inventarios ConsultarPorId(int id)
        {
            AbrirConexion();

            var inventario = iConexion!.Inventarios!
                
                .FirstOrDefault(i => i.Id == id);
            if (inventario == null)
                throw new Exception("No se encontró ningún inventario con ID " + id);

            RegistrarAuditoria(
                "Se consultó el inventario con ID: " + id,
                "Consulta por ID");

            return inventario;
        }
        public List<Inventarios> ConsultarPorUbicacion(string ubicacion)
        {
            AbrirConexion();

            var lista = iConexion!.Inventarios!
                .Where(i => i.Ubicacion!.ToLower().Contains(ubicacion.ToLower()))
                .ToList();

            RegistrarAuditoria(
                "Se consultaron inventarios por ubicación: " + ubicacion,
                "Consulta por Ubicacion");

            return lista;
        }

        public Inventarios AgregarStock(int inventarioId, int cantidad, decimal precioUnitario)
        {
            AbrirConexion();

            if (cantidad <= 0)
            {
                throw new Exception("La cantidad a agregar debe ser mayor a cero");
            }

            var inventario = iConexion!.Inventarios!.FirstOrDefault(i => i.Id == inventarioId);
            if (inventario == null)
            {
                throw new Exception("No se encontró ningún inventario con ID " + inventarioId);
            }

            inventario.Cantidad += cantidad;
            inventario.ValorTotal += cantidad * precioUnitario;
            inventario.FechaActualizacion = DateTime.Now;

            iConexion.Inventarios!.Update(inventario);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se agregaron " + cantidad + " unidades al inventario ID " + inventarioId,
                "Agregar Stock");

            return inventario;
        }

        public Inventarios ReducirStock(int inventarioId, int cantidad, decimal precioUnitario)
        {
            AbrirConexion();

            if (cantidad <= 0)
            {
                throw new Exception("La cantidad a reducir debe ser mayor a cero");
            }

            var inventario = iConexion!.Inventarios!.FirstOrDefault(i => i.Id == inventarioId);
            if (inventario == null)
            {
                throw new Exception("No se encontró ningún inventario con ID " + inventarioId);
            }

            if ((inventario.Cantidad ?? 0) < cantidad)
            {
                throw new Exception(
                    "Stock insuficiente. Disponible: " + inventario.Cantidad + ", solicitado: " + cantidad);
            }

            inventario.Cantidad -= cantidad;
            inventario.ValorTotal -= cantidad * precioUnitario;
            inventario.FechaActualizacion = DateTime.Now;

            if (inventario.ValorTotal < 0) inventario.ValorTotal = 0;

            iConexion.Inventarios!.Update(inventario);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se redujeron " + cantidad + " unidades del inventario ID " + inventarioId,
                "Reducir Stock");

            return inventario;
        }

        public Inventarios RecalcularValorTotal(int inventarioId)
        {
            AbrirConexion();

            var inventario = iConexion!.Inventarios!.FirstOrDefault(i => i.Id == inventarioId);
            if (inventario == null)
                throw new Exception("No se encontró ningún inventario con ID " + inventarioId);

            var autos = iConexion.Autos!
                .Where(a => a.Inventario!= null && a.Inventario.Id == inventarioId)
                .ToList();

            inventario.Cantidad = autos.Count;
            inventario.FechaActualizacion = DateTime.Now;

            iConexion.Inventarios!.Update(inventario);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se recalculó el inventario ID " + inventarioId + ": " + autos.Count + " autos",
                "Recalculo Inventario");

            return inventario;
        }

        public void ValidarDatos(Inventarios entidad)
        {
            if (entidad == null)
                throw new Exception("La información del inventario es obligatoria");

            if (string.IsNullOrEmpty(entidad.Ubicacion))
                throw new Exception("La ubicación del inventario es obligatoria");

            if (entidad.Cantidad < 0)
                throw new Exception("La cantidad del inventario no puede ser negativa");

            if (entidad.ValorTotal < 0)
                throw new Exception("El valor total del inventario no puede ser negativo");
        }
    }
}