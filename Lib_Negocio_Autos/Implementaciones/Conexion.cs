using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class Conexion : DbContext, IConexion
    {
        public string? string_conexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.string_conexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        // Faltan los Dbset de las clases 
        public DbSet<Personas>? Personas { get; set; }
        public DbSet<Auditorias>? Auditorias { get; set; }
        public DbSet<Sucursales>? Sucursales { get; set; }
        public DbSet<Parqueaderos>? Parqueaderos { get; set; }
        public DbSet<Talleres>? Talleres { get; set; }
        public DbSet<Inventarios>? Inventarios { get; set; } 
        public DbSet<Clientes>? Clientes { get; set; }
        public DbSet<Empleados>? Empleados { get; set; }

    }
}
