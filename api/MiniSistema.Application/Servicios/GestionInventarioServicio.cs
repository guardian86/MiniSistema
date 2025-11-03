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
        IEnumerable<Producto> productos = await _productoRepositorio.ObtenerTodosAsync();
        return productos.Select(MapearADto);
    }

    /// <inheritdoc />
    public async Task<ProductoDto> RegistrarMovimientoAsync(MovimientoRequestDto request)
    {
        Producto? producto = await _productoRepositorio.ObtenerPorIdAsync(request.ProductoId);
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

    /// <inheritdoc />
    public async Task<ProductoDto> RegistrarMovimientosAsync(MovimientoCrearRequestDto request)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.Nombre))
        {
            throw new ArgumentException("El nombre es obligatorio.", nameof(request));
        }

        Producto? producto = await _productoRepositorio.ObtenerPorNombreAsync(request.Nombre);

        if (producto is null)
        {
            if (request.Cantidad <= 0)
            {
                throw new KeyNotFoundException($"Producto con nombre '{request.Nombre}' no encontrado para salida/ajuste negativo.");
            }

            producto = new Producto { Nombre = request.Nombre, Cantidad = request.Cantidad };
            producto = await _productoRepositorio.CrearAsync(producto);
            return MapearADto(producto);
        }

        checked
        {
            producto.Cantidad += request.Cantidad;
        }

        await _productoRepositorio.ActualizarAsync(producto);
        return MapearADto(producto);
    }

    private static ProductoDto MapearADto(Producto p)
        => new(p.Id, p.Nombre, p.Cantidad);
}
