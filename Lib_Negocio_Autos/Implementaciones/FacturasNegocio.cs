using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class FacturasNegocio : IFacturasNegocio
    {
        private IConexion? iConexion;
        public List<Facturas> Consultar()
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var lista = iConexion.Facturas!.ToList();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo una consulta en Facturas";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Consulta";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();

            return lista;
        }

        public Facturas Guardar(Facturas entidad)
        {

            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            iConexion.Facturas!.Add(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo un guardado en Facturas";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Guardado";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public Facturas Eliminar(Facturas entidad)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            if (ValidarId(entidad.Id))
            {
                throw new Exception("El ID de factura no existe en el sistema");
            }

            iConexion.Facturas!.Remove(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se elimino un registro en Facturas";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Eliminacion";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public Facturas Modificar(Facturas entidad)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El ID de factura no existe en el sistema");
            }

            iConexion.Facturas!.Update(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se modifico un registro en Facturas";
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
            var factura = iConexion.Facturas!.FirstOrDefault(f => f.Id == id);
            return factura != null;
        }
        public double CalcularTotal(Facturas factura)
        {
            double subtotal = 0;

            if (factura.DetalleFactura != null)
            {
                subtotal = factura.DetalleFactura.Sum(x => x.Subtotal ?? 0);
            }

            double iva = subtotal * 0.19;

            factura.IVA = iva;
            factura.Total = subtotal + iva;

            return factura.Total;
        }

        public List<Facturas> ConsultarPorCliente(int clienteId)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var facturas = iConexion.Facturas!.Where(f => f._Clientes != null && f._Clientes.Id == clienteId).ToList();
            var Auditorias = new Auditorias();

            Auditorias.Descripcion = $"Se realizo una consulta por Cliente en Facturas para el Cliente ID: {clienteId}";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Consulta por Cliente";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return facturas;
        }

        public Facturas ConsultarPorId(int id)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            var factura = iConexion.Facturas!.FirstOrDefault(f => f.Id == id);
            var Auditorias = new Auditorias();

            Auditorias.Descripcion = $"Se realizo una consulta por ID en Facturas para el ID: {id}";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Consulta por ID";

            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return factura!;
        }

    }
}
