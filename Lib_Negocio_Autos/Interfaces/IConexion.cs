using Lib_Negocio_Autos.modelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IConexion
    {
        string? string_conexion { get; set; }
        // Falta agregar los Dbset 
        DbSet<Personas>? Persona { get; set; }
        DbSet<Auditorias>? Auditoria { get; set; }
        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
