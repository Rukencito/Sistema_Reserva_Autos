using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class VentasNegocio : IVentasNegocio
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
                Usuario = "UsuarioActual",
                Accion = accion
            });
            iConexion.SaveChanges();
        }

        public List<Ventas> Consultar()
        {
            AbrirConexion();

            var lista = iConexion!.Ventas!.ToList();

            RegistrarAuditoria("Se realizo una consulta en Ventas", "Consulta");
            return lista;
        }

        public Ventas Guardar(Ventas entidad)
        {

            AbrirConexion();

            iConexion!.Ventas!.Add(entidad!);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se guardo un nuevo registro en Ventas", "Creacion");
            return entidad;
        }

        public Ventas Eliminar(Ventas entidad)
        {
            AbrirConexion();

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El registro no existe");
            }
            iConexion!.Ventas!.Remove(entidad!);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se elimino un registro en Ventas", "Eliminacion");
            return entidad;
        }

        public Ventas Modificar(Ventas entidad)
        {
            AbrirConexion();

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El registro no existe");
            }
            iConexion!.Ventas!.Update(entidad!);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se modifico un registro en Ventas", "Modificacion");
            return entidad;
        }

        public bool ValidarId(int id)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            var Ventas = iConexion.Ventas!.FirstOrDefault(v => v.Id == id);
            return Ventas != null;
        }

        public List<Ventas> ConsultarPorCliente(int idCliente)
        {
            AbrirConexion();

            var lista = iConexion!.Ventas!
                .Where(v => v.Clientes == idCliente)
                .ToList();

            if (lista.Count == 0)
                throw new Exception($"No se encontraron ventas para el cliente con id: {idCliente}");

            RegistrarAuditoria(
                $"Se realizo una consulta en Ventas por el cliente con id: {idCliente}",
                "Consulta"
            );
            return lista;
        }

        public void ValidarDatos(Ventas entidad)
        {
            if (entidad == null)
                throw new Exception("La información de la venta es obligatoria");

            if (string.IsNullOrEmpty(entidad.TipoPago))
                throw new Exception("El tipo de pago es obligatorio");

            if (entidad.PrecioVenta <= 0)
                throw new Exception("El precio de venta debe ser mayor a 0");

            if (entidad.FechaVenta == default)
                throw new Exception("La fecha de venta es obligatoria");
        }

    }
}
