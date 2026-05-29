using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class DevolucionesNegocio : IDevolucionesNegocio
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

        public List<Devoluciones> Consultar()
        {
            AbrirConexion();
            var lista = iConexion!.Devoluciones!.ToList();

            foreach (var devolucion in lista)
            {
                devolucion.Alquiler = iConexion.Alquileres!
                    .FirstOrDefault(a => a.Id == devolucion.Alquileres);
            }

            RegistrarAuditoria("Se realizó una consulta en Devoluciones", "Consulta");
            return lista;
        }

        public Devoluciones Guardar(Devoluciones entidad)
        {
            AbrirConexion();
            ValidarDatos(entidad);

            if (ExisteDevolucionParaAlquiler(entidad.Alquileres))
            {
                throw new Exception("Ya existe una devolución registrada para ese alquiler");
            }

            iConexion!.Devoluciones!.Add(entidad);
            iConexion.SaveChanges();

            CerrarAlquiler(entidad.Alquileres);

            RegistrarAuditoria(
                "Se registró la devolución del alquiler ID " + entidad.Alquileres,
                "Guardado");

            return entidad;
        }

        public Devoluciones Eliminar(Devoluciones entidad)
        {
            AbrirConexion();

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("La devolución con ID " + entidad.Id + " no existe en el sistema");
            }

            iConexion!.Devoluciones!.Remove(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se eliminó la devolución con ID " + entidad.Id,
                "Eliminacion");

            return entidad;
        }

        public Devoluciones Modificar(Devoluciones entidad)
        {
            AbrirConexion();

            ValidarDatos(entidad);

            if (!ValidarId(entidad.Id))
            {
                throw new Exception("La devolución con ID " + entidad.Id + " no existe en el sistema");
            }

            iConexion!.Devoluciones!.Update(entidad);
            iConexion.SaveChanges();

            RegistrarAuditoria(
                "Se modificó la devolución con ID " + entidad.Id,
                "Modificacion");

            return entidad;
        }

        public bool ValidarId(int id)
        {
            if (iConexion == null) AbrirConexion();
            return iConexion!.Devoluciones!.Any(d => d.Id == id);
        }

        public bool ExisteDevolucionParaAlquiler(int alquilerId)
        {
            if (iConexion == null)
            {
                AbrirConexion();
            }

            return iConexion!.Devoluciones!
                .Any(d => d.Alquileres == alquilerId);
        }

        public Devoluciones ConsultarPorAlquiler(int alquilerId)
        {
            AbrirConexion();

            var devolucion = iConexion!.Devoluciones!
                .FirstOrDefault(d => d.Alquileres == alquilerId);

            if (devolucion == null)
            {
                throw new Exception("No se encontró devolución para el alquiler con ID " + alquilerId);
            }

            RegistrarAuditoria(
                "Se consultó la devolución del alquiler con ID: " + alquilerId,
                "Consulta por Alquiler");

            return devolucion;
        }

        public Devoluciones ConsultarPorId(int id)
        {
            AbrirConexion();

            var devolucion = iConexion!.Devoluciones!
                .Include(d => d.Alquiler) 
                .FirstOrDefault(d => d.Id == id);
            if (devolucion == null)
            {
                throw new Exception("No se encontró ninguna devolución con ID " + id);
            }

            RegistrarAuditoria(
                "Se consultó la devolución con ID: " + id,
                "Consulta por ID");

            return devolucion;
        }

        private void CerrarAlquiler(int alquilerId)
        {
            var alquiler = iConexion!.Alquileres!.FirstOrDefault(a => a.Id == alquilerId);
            if (alquiler != null)
            {
                alquiler.EstadoAlquiler = false;
                iConexion.Alquileres!.Update(alquiler);
                iConexion.SaveChanges();

                RegistrarAuditoria(
                    "Se cerró el alquiler con ID " + alquilerId + " por devolución registrada",
                    "Cierre de Alquiler");
            }
        }

     
        public void ValidarDatos(Devoluciones entidad)
        {
            if (entidad == null)
                throw new Exception("La información de la devolución es obligatoria");

            if (entidad.Alquileres == 0)
                throw new Exception("La devolución debe estar asociada a un alquiler");

            if (entidad.FechaEntrega == DateTime.MinValue)
                throw new Exception("La fecha de entrega es obligatoria");

            if (entidad.Alquileres == 0)
                throw new Exception("La devolución debe estar asociada a un alquiler");

            if (entidad.NivelCombustible < 0 || entidad.NivelCombustible > 100)
                throw new Exception("El nivel de combustible debe estar entre 0 y 100");

            if (entidad.Kilometraje < 0)
                throw new Exception("El kilometraje no puede ser un valor negativo");
        }
    }
}