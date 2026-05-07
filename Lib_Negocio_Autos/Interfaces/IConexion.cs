using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IConexion
    {
        string? string_conexion { get; set; }
        // Falta agregar los Dbset 

        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
