using Microsoft.EntityFrameworkCore;
using MiniSistema.Domain.Entidades;

namespace MiniSistema.Infrastructure.Persistencia;

/// <summary>
/// DbContext de EF Core para el MiniSistema.
/// </summary>
public class MiniSistemaDbContext : DbContext
{
    public MiniSistemaDbContext(DbContextOptions<MiniSistemaDbContext> options) : base(options)
    {
    }

    public DbSet<Producto> Productos => Set<Producto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Producto>(entity =>
      {
        // Mapeo al esquema final requerido: tabla productos (id, nombre, cantidad)
        entity.ToTable("productos");
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();
        entity.Property(e => e.Nombre)
            .HasColumnName("nombre")
            .HasMaxLength(200)
            .IsRequired();
        entity.Property(e => e.Cantidad)
            .HasColumnName("cantidad");
      });
    }
}
