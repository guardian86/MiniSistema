using MiniSistema.Application.Dtos;

namespace MiniSistema.Application.Servicios;

/// <summary>
/// Contrato de servicio de aplicación para la gestión de inventario.
/// </summary>
public interface IGestionInventarioServicio
{
    /// <summary>
    /// Consulta el inventario actual.
    /// </summary>
    /// <returns>Listado de productos en formato DTO.</returns>
    Task<IEnumerable<ProductoDto>> ConsultarInventarioAsync();

    /// <summary>
    /// Registra un movimiento (entrada/salida) de un producto.
    /// </summary>
    /// <param name="request">Datos del movimiento a registrar.</param>
    /// <returns>Producto actualizado en formato DTO.</returns>
    Task<ProductoDto> RegistrarMovimientoAsync(MovimientoRequestDto request);

    /// <summary>
    /// Registra un movimiento referenciando el producto por nombre. Si no existe y la cantidad es positiva, lo crea.
    /// </summary>
    Task<ProductoDto> RegistrarMovimientosAsync(MovimientoCrearRequestDto request);
}
