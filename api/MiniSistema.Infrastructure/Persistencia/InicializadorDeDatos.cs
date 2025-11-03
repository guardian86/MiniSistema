using MiniSistema.Domain.Entidades;

namespace MiniSistema.Infrastructure.Persistencia;

/// <summary>
/// Inicializa datos mínimos si la base está vacía.
/// </summary>
public static class InicializadorDeDatos
{
    public static async Task InicializarAsync(MiniSistemaDbContext db)
    {
        await db.Database.EnsureCreatedAsync();

        if (!db.Productos.Any())
        {
            db.Productos.AddRange(
                new Producto { Nombre = "Lapicero", Cantidad = 10 },
                new Producto { Nombre = "Cuaderno", Cantidad = 5 }
            );
            await db.SaveChangesAsync();
        }
    }
}
