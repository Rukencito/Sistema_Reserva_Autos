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
        public List<Ventas> Consultar()
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var lista = iConexion.Ventas!.ToList();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo una consulta en Ventas";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Consulta";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();

            return lista;
        }

        public Ventas Guardar(Ventas entidad)
        {

            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            iConexion.Ventas!.Add(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo un guardado en Ventas";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Guardado";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public Ventas Eliminar(Ventas entidad)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El registro no existe");
            }
            iConexion.Ventas!.Remove(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se elimino un registro en Ventas";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Eliminacion";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public Ventas Modificar(Ventas entidad)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El registro no existe");
            }
            iConexion.Ventas!.Update(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se modifico un registro en Ventas";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Modificacion";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public bool ValidarId(int id)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            var Alquiler = iConexion.Alquileres!.FirstOrDefault(a => a.Id == id);
            return Alquiler != null;
        }

        public void ConsultarporCliente(int idCliente)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            var lista = iConexion.Ventas!.Where(v => v._Clientes!.Id == idCliente).ToList();
            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo una consulta en Ventas por Cliente";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; 
            Auditorias.Accion = "Consulta por Cliente";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
        }

    }
}
