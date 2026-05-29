using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class DuenosNegocio : IDuenosNegocio
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

        public List<Duenos> Consultar()
        {
            AbrirConexion();
            var lista = iConexion!.Duenos!.ToList();
            RegistrarAuditoria("Se realizó una consulta en Duenos", "Consulta");
            return lista;
        }

        public Duenos Guardar(Duenos entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (ValidarCedula(entidad.Cedula!))
            {
                throw new Exception("Ya existe un dueño registrado con la cédula " + entidad.Cedula);
            }

            iConexion!.Duenos!.Add(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se guardó el dueño con cédula " + entidad.Cedula, "Guardado");
            return entidad;
        }

        public Duenos Eliminar(Duenos entidad)
        {
            AbrirConexion();

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El dueño con ID " + entidad.Id + " no existe en el sistema");
            }

            iConexion!.Duenos!.Remove(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se eliminó el dueño con ID " + entidad.Id, "Eliminacion");
            return entidad;
        }

        public Duenos Modificar(Duenos entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("El dueño con ID " + entidad.Id + " no existe en el sistema");
            }

            iConexion!.Duenos!.Update(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria("Se modificó el dueño con ID " + entidad.Id, "Modificacion");
            return entidad;
        }

        public bool ValidarId(int id)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.Duenos!.Any(d => d.Id == id);
        }

        public bool ValidarCedula(string cedula)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.Duenos!.Any(d => d.Cedula == cedula);
        }

        public Duenos ConsultarPorCedula(string cedula)
        {
            AbrirConexion();

            var dueno = iConexion!.Duenos!.FirstOrDefault(d => d.Cedula == cedula);

            if (dueno == null)
            {
                throw new Exception("No se encontró ningún dueño con la cédula " + cedula);
            }

            RegistrarAuditoria("Se consultó dueño por cédula: " + cedula, "Consulta por Cédula");
            return dueno;
        }

        public bool VerificarEstadoDueno(int duenoId)
        {
            AbrirConexion();

            var dueno = iConexion!.Duenos!.FirstOrDefault(d => d.Id == duenoId);
            if (dueno == null)
            {
                throw new Exception("No se encontró ningún dueño con ID " + duenoId);
            }

            if (dueno.Estado == false)
            {
                throw new Exception("El dueño con ID " + duenoId + " no se encuentra activo");
            }

            RegistrarAuditoria(
                "Se verificó el estado del dueño con ID " + duenoId,
                "Verificacion Estado");

            return true;
        }

        public Duenos AgregarAuto(int duenoId)
        {
            AbrirConexion();

            var dueno = iConexion!.Duenos!.FirstOrDefault(d => d.Id == duenoId);
            if (dueno == null)
            {
                throw new Exception("No se encontró ningún dueño con ID " + duenoId);
            }

            if (dueno.Estado == false)
            {
                throw new Exception("No se puede agregar un auto a un dueño inactivo");
            }

            dueno.CantidadAutos = (dueno.CantidadAutos ?? 0) + 1;
            iConexion.Duenos!.Update(dueno);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se incrementó la cantidad de autos del dueño ID " + duenoId,
                "Agregar Auto");

            return dueno;
        }

        public Duenos QuitarAuto(int duenoId)
        {
            AbrirConexion();

            var dueno = iConexion!.Duenos!.FirstOrDefault(d => d.Id == duenoId);
            if (dueno == null)
            {
                throw new Exception("No se encontró ningún dueño con ID " + duenoId);
            }

            if ((dueno.CantidadAutos ?? 0) <= 0)
            {
                throw new Exception("El dueño con ID " + duenoId + " no tiene autos registrados");
            }

            dueno.CantidadAutos -= 1;
            iConexion.Duenos!.Update(dueno);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se decrementó la cantidad de autos del dueño ID " + duenoId,
                "Quitar Auto");

            return dueno;
        }
        public void ValidarDatos(Duenos entidad)
        {
            if (entidad == null)
                throw new Exception("La información del dueño es obligatoria");

            if (string.IsNullOrEmpty(entidad.Nombre))
                throw new Exception("El nombre del dueño es obligatorio");

            if (string.IsNullOrEmpty(entidad.Apellido))
                throw new Exception("El apellido del dueño es obligatorio");

            if (string.IsNullOrEmpty(entidad.Cedula))
                throw new Exception("La cédula del dueño es obligatoria");

            if (entidad.Edad < 18)
                throw new Exception("El dueño debe ser mayor de edad");

            if (string.IsNullOrEmpty(entidad.Correo))
                throw new Exception("El correo del dueño es obligatorio");

            if (!entidad.Correo.Contains("@") || !entidad.Correo.Contains("."))
                throw new Exception("El correo del dueño no tiene un formato válido");

            if (string.IsNullOrEmpty(entidad.Telefono))
                throw new Exception("El teléfono del dueño es obligatorio");

            if (entidad.CantidadAutos < 0)
                throw new Exception("La cantidad de autos no puede ser negativa");
        }
    }
}