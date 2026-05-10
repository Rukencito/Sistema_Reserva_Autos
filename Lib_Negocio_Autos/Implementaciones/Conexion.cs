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
        public DbSet<Sucursales>? Sucursales { get; set; }
        public DbSet<Parqueaderos>? Parqueaderos { get; set; }
        public DbSet<Talleres>? Talleres { get; set; }
        public DbSet<Inventarios>? Inventarios { get; set; }
        public DbSet<Roles>? Roles { get; set; }
        public DbSet<Auditorias>? Auditorias { get; set; }
        public DbSet<Clientes>? Clientes { get; set; }
        public DbSet<Empleados>? Empleados { get; set; }
        public DbSet<Duenos>? Duenos { get; set; }
        public DbSet<Ventas>? Ventas { get; set; }
        public DbSet<Autos>? Autos { get; set; }
        public DbSet<Alquileres>? Alquileres { get; set; }
        public DbSet<Garantias>? Garantias { get; set; }
        public DbSet<Seguros>? Seguros { get; set; }
        public DbSet<Mantenimientos>? Mantenimientos { get; set; }
        public DbSet<Reservas>? Reservas { get; set; }
        public DbSet<Devoluciones>? Devoluciones { get; set; }
        public DbSet<Contratos>? Contratos { get; set; }
        public DbSet<Promociones>? Promociones { get; set; }
        public DbSet<Facturas>? Facturas { get; set; }
        public DbSet<Reseñas>? Reseñas { get; set; }
        public DbSet<DetallesFactura>? DetallesFactura { get; set; }
        public DbSet<Pagos>? Pagos { get; set; }
        public DbSet<Usuarios>? Usuarios { get; set; }
        public DbSet<Permisos>? Permisos { get; set; }

    }
}
