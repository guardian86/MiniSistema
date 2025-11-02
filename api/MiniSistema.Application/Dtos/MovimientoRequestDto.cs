namespace MiniSistema.Application.Dtos;

/// <summary>
/// Solicitud para registrar un movimiento de inventario (entrada/salida).
/// </summary>
/// <param name="ProductoId">Identificador del producto a ajustar.</param>
/// <param name="CantidadAjuste">Cantidad a ajustar (positiva para entrada, negativa para salida).</param>
public readonly record struct MovimientoRequestDto(int ProductoId, int CantidadAjuste);
