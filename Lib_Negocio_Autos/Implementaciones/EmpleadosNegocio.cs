using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class EmpleadosNegocio : IEmpleadosNegocio
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

        public List<Empleados> Consultar()
        {
            AbrirConexion();
            var lista = iConexion!.Empleados!
                .Include(e => e.Sucursal) 
                .ToList();
            RegistrarAuditoria("Se realizó una consulta en Empleados", "Consulta");
            return lista;
        }

        public Empleados Guardar(Empleados entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (ValidarCedula(entidad.Cedula!))
            {
                throw new Exception("Ya existe un empleado registrado con la cédula " + entidad.Cedula);
            }

            iConexion!.Empleados!.Add(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se guardó el empleado con cédula " + entidad.Cedula,
                "Guardado");

            return entidad;
        }

        public Empleados Eliminar(Empleados entidad)
        {
            AbrirConexion();

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El empleado con ID " + entidad.Id + " no existe en el sistema");
            }

            iConexion!.Empleados!.Remove(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se eliminó el empleado con ID " + entidad.Id,
                "Eliminacion");

            return entidad;
        }

        public Empleados Modificar(Empleados entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El empleado con ID " + entidad.Id + " no existe en el sistema");
            }

            iConexion!.Empleados!.Update(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se modificó el empleado con ID " + entidad.Id,
                "Modificacion");

            return entidad;
        }

        public bool ValidarId(int id)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.Empleados!.Any(e => e.Id == id);
        }
        public bool ValidarCedula(string cedula)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.Empleados!.Any(e => e.Cedula == cedula);
        }
        public Empleados ConsultarPorCedula(string cedula)
        {
            AbrirConexion();

            var empleado = iConexion!.Empleados!.FirstOrDefault(e => e.Cedula == cedula);

            if (empleado == null)
            {
                throw new Exception("No se encontró ningún empleado con la cédula " + cedula);
            }

            RegistrarAuditoria(
                "Se consultó empleado por cédula: " + cedula,
                "Consulta por Cédula");

            return empleado;
        }

        public List<Empleados> ConsultarPorCargo(string cargo)
        {
            AbrirConexion();

            var lista = iConexion!.Empleados!
                .Where(e => e.Cargo!.ToLower().Contains(cargo.ToLower()))
                .ToList();

            RegistrarAuditoria(
                "Se consultaron empleados por cargo: " + cargo,
                "Consulta por Cargo");

            return lista;
        }

        public List<Empleados> ConsultarPorSucursal(int sucursalId)
        {
            AbrirConexion();

            var lista = iConexion!.Empleados!
                .Where(e => e.Sucursal!= null && e.Sucursal.Id == sucursalId)
                .ToList();

            RegistrarAuditoria(
                "Se consultaron empleados de la sucursal ID: " + sucursalId,
                "Consulta por Sucursal");

            return lista;
        }

        public decimal CalcularSalarioTotal(int empleadoId)
        {
            AbrirConexion();

            var empleado = iConexion!.Empleados!.FirstOrDefault(e => e.Id == empleadoId);
            if (empleado == null)
            {
                throw new Exception("No se encontró ningún empleado con ID " + empleadoId);
            }

            decimal total = (empleado.Salario + (empleado.Bonificaciones ?? 0));

            RegistrarAuditoria(
                "Se calculó el salario total del empleado ID " + empleadoId + ": " + total,
                "Calculo Salario Total");

            return total;
        }

        public void ValidarDatos(Empleados entidad)
        {
            if (entidad == null)
                throw new Exception("La información del empleado es obligatoria");

            if (string.IsNullOrEmpty(entidad.Nombre))
                throw new Exception("El nombre del empleado es obligatorio");

            if (string.IsNullOrEmpty(entidad.Apellido))
                throw new Exception("El apellido del empleado es obligatorio");

            if (string.IsNullOrEmpty(entidad.Cedula))
                throw new Exception("La cédula del empleado es obligatoria");

            if (entidad.Edad < 18)
                throw new Exception("El empleado debe ser mayor de edad");

            if (string.IsNullOrEmpty(entidad.Correo))
                throw new Exception("El correo del empleado es obligatorio");

            if (!entidad.Correo.Contains("@") || !entidad.Correo.Contains("."))
                throw new Exception("El correo del empleado no tiene un formato válido");

            if (string.IsNullOrEmpty(entidad.Telefono))
                throw new Exception("El teléfono del empleado es obligatorio");

            if (string.IsNullOrEmpty(entidad.Cargo))
                throw new Exception("El cargo del empleado es obligatorio");

            if (string.IsNullOrEmpty(entidad.Horario))
                throw new Exception("El horario del empleado es obligatorio");

            if (entidad.Salario <= 0)
                throw new Exception("El salario del empleado debe ser mayor a cero");

            if (entidad.Bonificaciones < 0)
                throw new Exception("Las bonificaciones no pueden ser un valor negativo");
        }
    }
}