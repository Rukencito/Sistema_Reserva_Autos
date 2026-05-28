using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class PromocionesNegocio : IPromocionesNegocio
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

        public List<Promociones> Consultar()
        {

            AbrirConexion();
            var lista = iConexion!.Promociones!.ToList();

            foreach (var promocion in lista)
            {
                promocion.Venta = iConexion.Ventas!
                    .FirstOrDefault(v => v.Id == promocion.Ventas);
            }

            RegistrarAuditoria("Se realizó una consulta en Promociones", "Consulta");
            return lista;
        }

        public Promociones Guardar(Promociones entidad)
        {

            AbrirConexion();
            iConexion!.Promociones!.Add(entidad!);
            iConexion.SaveChanges();
            RegistrarAuditoria("Se guardó un nuevo registro en Promociones", "Creacion");
            return entidad;
        }

        public Promociones Eliminar(Promociones entidad)
        {
            AbrirConexion();
            iConexion!.Promociones!.Remove(entidad!);
            iConexion.SaveChanges();
            RegistrarAuditoria("Se elimino un registro en promociones", "Eliminacion");
            return entidad;
        }

        public Promociones Modificar(Promociones entidad)
        {
            AbrirConexion();
            iConexion!.Promociones!.Update(entidad!);
            iConexion.SaveChanges();
            RegistrarAuditoria("Se modifico un registro en promociones", "Modificacion");
            return entidad;
        }

        public bool ValidarId(int id)
        {
            AbrirConexion();

            var promocion = iConexion!.Promociones!.FirstOrDefault(p => p.Id == id);
            return promocion != null;
        }

        public void ValidarDatos(Promociones entidad)
        {
            if (entidad == null)
                throw new Exception("La información de la promoción es obligatoria");

            if (string.IsNullOrEmpty(entidad.Descripcion))
                throw new Exception("La descripción de la promoción es obligatoria");

            if (entidad.Descuento == null || entidad.Descuento <= 0 || entidad.Descuento > 100)
                throw new Exception("El descuento debe ser un valor entre 1 y 100");

            if (entidad.FechaInicio == default)
                throw new Exception("La fecha de inicio de la promoción es obligatoria");

            if (entidad.FechaFin == default)
                throw new Exception("La fecha de fin de la promoción es obligatoria");

            if (entidad.FechaFin <= entidad.FechaInicio)
                throw new Exception("La fecha de fin debe ser mayor a la fecha de inicio");
        }
    }


}
