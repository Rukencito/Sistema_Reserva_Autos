using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class ClientesNegocio : IClientesNegocio
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

        public List<Clientes> Consultar()
        {
            AbrirConexion();
            var lista = iConexion!.Clientes!.ToList();
            RegistrarAuditoria("Se realizó una consulta en Clientes", "Consulta");
            return lista;
        }

        public Clientes Guardar(Clientes entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (ValidarCedula(entidad.Cedula!))
            {
                throw new Exception("Ya existe un cliente registrado con la cédula " + entidad.Cedula);
            }

            iConexion!.Clientes!.Add(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se guardó el cliente con cédula " + entidad.Cedula, "Guardado");
            return entidad;
        }

        public Clientes Eliminar(Clientes entidad)
        {
            AbrirConexion();

            if (!ValidarCedula(entidad.Cedula!))
            {
                throw new Exception("El cliente con cédula " + entidad.Cedula + " no existe en el sistema");
            }

            if (iConexion!.Alquileres!.Any(x => x.Clientes == entidad.Id))
                throw new Exception("No se puede eliminar el cliente porque tiene alquileres registrados");

            if (iConexion.Reservas!.Any(x => x.Clientes == entidad.Id))
                throw new Exception("No se puede eliminar el cliente porque tiene reservas registradas");

            if (iConexion.Facturas!.Any(x => x.Clientes == entidad.Id))
                throw new Exception("No se puede eliminar el cliente porque tiene facturas registradas");

            if (iConexion.Resenas!.Any(x => x.Clientes == entidad.Id))
                throw new Exception("No se puede eliminar el cliente porque tiene reseñas registradas");

            if (iConexion.Ventas!.Any(x => x.Clientes == entidad.Id))
                throw new Exception("No se puede eliminar el cliente porque tiene ventas registradas");

            if (iConexion.Usuarios!.Any(x => x.Clientes == entidad.Id))
                throw new Exception("No se puede eliminar el cliente porque tiene un usuario asociado");

            iConexion!.Clientes!.Remove(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se eliminó el cliente con cédula " + entidad.Cedula, "Eliminacion");
            return entidad;
        }

        public Clientes Modificar(Clientes entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (!ValidarCedula(entidad.Cedula!))
            {
                throw new Exception("El cliente con cédula " + entidad.Cedula + " no existe en el sistema");
            }

            iConexion!.Clientes!.Update(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se modificó el cliente con cédula " + entidad.Cedula, "Modificacion");
            return entidad;
        }


        public bool ValidarCedula(string cedula)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.Clientes!.Any(c => c.Cedula == cedula);
        }

        public Clientes ConsultarPorCedula(string cedula)
        {
            AbrirConexion();

            var cliente = iConexion!.Clientes!.FirstOrDefault(c => c.Cedula == cedula);

            if (cliente == null)
            {
                throw new Exception("No se encontró ningún cliente con la cédula " + cedula);
            }

            RegistrarAuditoria("Se consultó cliente por cédula: " + cedula, "Consulta por Cédula");
            return cliente;
        }
        public Clientes AgregarPuntosFidelidad(int clienteId, int puntos)
        {
            AbrirConexion();

            if (puntos <= 0)
            {
                throw new Exception("Los puntos a agregar deben ser un valor positivo");
            }

            var cliente = iConexion!.Clientes!.FirstOrDefault(c => c.Id == clienteId);
            if (cliente == null)
            {
                throw new Exception("No se encontró ningún cliente con ID " + clienteId);
            }

            cliente.PuntosFidelidad = (cliente.PuntosFidelidad ?? 0) + puntos;
            iConexion.Clientes!.Update(cliente);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se agregaron " + puntos + " puntos al cliente con ID " + clienteId,
                "Agregar Puntos Fidelidad");

            return cliente;
        }

        public bool TieneLicencia(int clienteId)
        {
            AbrirConexion();

            var cliente = iConexion!.Clientes!.FirstOrDefault(c => c.Id == clienteId);
            if (cliente == null)
            {
                throw new Exception("No se encontró ningún cliente con ID " + clienteId);
            }

            RegistrarAuditoria(
                "Se verificó licencia del cliente con ID " + clienteId,
                "Verificacion Licencia");

            return cliente.LicenciaConduccion == true;
        }

    
        public void ValidarDatos(Clientes entidad)
        {
            if (entidad == null)
                throw new Exception("La información del cliente es obligatoria");

            if (string.IsNullOrEmpty(entidad.Nombre))
                throw new Exception("El nombre del cliente es obligatorio");

            if (string.IsNullOrEmpty(entidad.Apellido))
                throw new Exception("El apellido del cliente es obligatorio");

            if (string.IsNullOrEmpty(entidad.Cedula))
                throw new Exception("La cédula del cliente es obligatoria");

            if (entidad.Edad < 18)
                throw new Exception("El cliente debe ser mayor de edad");

            if (string.IsNullOrEmpty(entidad.Correo))
                throw new Exception("El correo del cliente es obligatorio");

            if (!entidad.Correo.Contains("@") || !entidad.Correo.Contains("."))
                throw new Exception("El correo del cliente no tiene un formato válido");

            if (entidad.LicenciaConduccion == false)
                throw new Exception("El cliente debe tener licencia de conducción vigente");
        }
    }
}