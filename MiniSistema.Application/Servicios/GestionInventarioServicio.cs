using MiniSistema.Application.Dtos;
using MiniSistema.Domain.Entidades;
using MiniSistema.Domain.Interfaces;

namespace MiniSistema.Application.Servicios;

/// <summary>
/// Implementación del servicio de gestión de inventario.
/// </summary>
public sealed class GestionInventarioServicio : IGestionInventarioServicio
{
    private readonly IProductoRepositorio _productoRepositorio;

    public GestionInventarioServicio(IProductoRepositorio productoRepositorio)
    {
        _productoRepositorio = productoRepositorio ?? throw new ArgumentNullException(nameof(productoRepositorio));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ProductoDto>> ConsultarInventarioAsync()
    {
        IEnumerable<Producto> productos = await _productoRepositorio.ObtenerTodosAsync().ConfigureAwait(false);
        return productos.Select(MapearADto);
    }

    /// <inheritdoc />
    public async Task<ProductoDto> RegistrarMovimientoAsync(MovimientoRequestDto request)
    {
        Producto? producto = await _productoRepositorio.ObtenerPorIdAsync(request.ProductoId).ConfigureAwait(false);
        if (producto is null)
        {
            throw new KeyNotFoundException($"Producto con Id {request.ProductoId} no encontrado.");
        }

        checked
        {
            producto.Cantidad += request.CantidadAjuste;
        }

        await _productoRepositorio.ActualizarAsync(producto).ConfigureAwait(false);
        return MapearADto(producto);
    }

    private static ProductoDto MapearADto(Producto p)
        => new(p.Id, p.Nombre, p.Cantidad);
}
