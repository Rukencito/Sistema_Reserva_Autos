using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class DetallesFacturaNegocio : IDetallesFacturaNegocio
    {
        private IConexion? iConexion;
        public List<DetallesFactura> Consultar()
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var lista = iConexion.DetallesFactura!.ToList();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo una consulta en DetallesFactura";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Consulta";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();

            return lista;
        }

        public DetallesFactura Guardar(DetallesFactura entidad)
        {

            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            ValidarDatos(entidad);

            iConexion.DetallesFactura!.Add(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo un guardado en DetallesFactura";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Guardado";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public DetallesFactura Eliminar(DetallesFactura entidad)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            if (ValidarId(entidad.Id))
            {
                throw new Exception("El ID del detalle de factura no existe en el sistema");
            }

            iConexion.DetallesFactura!.Remove(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se elimino un registro en DetallesFactura";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Eliminacion";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public DetallesFactura Modificar(DetallesFactura entidad)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El ID del detalle de factura no existe en el sistema");
            }

            iConexion.DetallesFactura!.Update(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se modifico un registro en DetallesFactura";
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
            return iConexion.DetallesFactura!.Any(e => e.Id == id);
        }

        public List<DetallesFactura> ConsultarPorTipoFactura(string tipoFactura)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var lista = iConexion.DetallesFactura!.Where(e => e.TipoFactura == tipoFactura).ToList();
            var Auditorias = new Auditorias();

            Auditorias.Descripcion = "Se realizo una consulta por TipoFactura en DetallesFactura con TipoFactura: {tipoFactura}";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Consulta por TipoFactura";

            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return lista;
        }

        public DetallesFactura ConsultarPorId(int id)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var entidad = iConexion.DetallesFactura!.FirstOrDefault(e => e.Id == id);
            var Auditorias = new Auditorias();

            Auditorias.Descripcion = $"Se realizo una consulta por ID en DetallesFactura con ID: {id}";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Consulta por ID";

            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad!;
        }
        public void ValidarDatos(DetallesFactura entidad)
        {
            if (entidad.Subtotal == null || entidad.Subtotal <= 0)
            {
                throw new ArgumentException("El subtotal debe ser un valor positivo.");
            }
            if (string.IsNullOrEmpty(entidad.TipoFactura))
            {
                throw new ArgumentException("El tipo de factura no puede estar vacío.");
            }
        }
    }
}
