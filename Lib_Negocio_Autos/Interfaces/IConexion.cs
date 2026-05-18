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
        DbSet<Sucursales>? Sucursales { get; set; }
        DbSet<Parqueaderos>? Parqueaderos { get; set; }
        DbSet<Talleres>? Talleres { get; set; }
        DbSet<Inventarios>? Inventarios { get; set; }
        DbSet<Roles>? Roles { get; set; }
        DbSet<Auditorias>? Auditorias { get; set; }
        DbSet<Clientes>? Clientes { get; set; }
        DbSet<Empleados>? Empleados { get; set; }
        DbSet<Duenos>? Duenos { get; set; }
        DbSet<Ventas>? Ventas { get; set; }
        DbSet<Autos>? Autos { get; set; }
        DbSet<Alquileres>? Alquileres { get; set; }
        DbSet<Garantias>? Garantias { get; set; }
        DbSet<Seguros>? Seguros { get; set; }
        DbSet<Mantenimientos>? Mantenimientos { get; set; }
        DbSet<Reservas>? Reservas { get; set; }
        DbSet<Devoluciones>? Devoluciones { get; set; }
        DbSet<Contratos>? Contratos { get; set; }
        DbSet<Promociones>? Promociones { get; set; }
        DbSet<Facturas>? Facturas { get; set; }
        DbSet<Reseñas>? Reseñas { get; set; }
        DbSet<DetallesFactura>? DetallesFactura { get; set; }
        DbSet<Pagos>? Pagos { get; set; }
        DbSet<Usuarios>? Usuarios { get; set; }
        DbSet<Permisos>? Permisos { get; set; }

        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
