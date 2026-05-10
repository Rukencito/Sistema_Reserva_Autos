using Lib_Negocio_Autos.modelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lib_Negocio_Autos.Interfaces
{
    public interface IConexion
    {
        string? string_conexion { get; set; }
        // Falta agregar los Dbset 
        DbSet<Personas>? Personas { get; set; }
        DbSet<Auditorias>? Auditorias { get; set; }
        DbSet<Sucursales>? Sucursales { get; set; } 
        DbSet<Parqueaderos>? Parqueaderos { get; set; }
        DbSet<Talleres>? Talleres { get; set; }
        DbSet<Inventarios>? Inventarios { get; set; }
        DbSet<Clientes>? Clientes { get; set; }
        DbSet<Empleados>? Empleados { get; set; }
        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
