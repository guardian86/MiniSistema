using Microsoft.EntityFrameworkCore;
using MiniSistema.Domain.Entidades;
using MiniSistema.Domain.Interfaces;
using MiniSistema.Infrastructure.Persistencia;

namespace MiniSistema.Infrastructure.Repositorios;

/// <summary>
/// Implementación de IProductoRepositorio con EF Core.
/// </summary>
public sealed class ProductoRepositorio : IProductoRepositorio
{
    private readonly MiniSistemaDbContext _db;

    public ProductoRepositorio(MiniSistemaDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task ActualizarAsync(Producto producto)
    {
        _db.Productos.Update(producto);
        await _db.SaveChangesAsync().ConfigureAwait(false);
    }

    public Task<Producto?> ObtenerPorIdAsync(int id)
        => _db.Productos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

    public Task<Producto?> ObtenerPorNombreAsync(string nombre)
        => _db.Productos.AsNoTracking().FirstOrDefaultAsync(p => p.Nombre == nombre);

    public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
        => await _db.Productos.AsNoTracking().OrderBy(p => p.Id).ToListAsync().ConfigureAwait(false);

    public async Task<Producto> CrearAsync(Producto producto)
    {
        await _db.Productos.AddAsync(producto).ConfigureAwait(false);
        await _db.SaveChangesAsync().ConfigureAwait(false);
        return producto;
    }
}
