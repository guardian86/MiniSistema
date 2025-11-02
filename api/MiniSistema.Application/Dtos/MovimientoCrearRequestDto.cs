namespace MiniSistema.Application.Dtos;

/// <summary>
/// Solicitud para registrar un movimiento (entrada/salida) referenciando el producto por nombre.
/// </summary>
public sealed class MovimientoCrearRequestDto
{
    public string Nombre { get; init; } = string.Empty;
    public int Cantidad { get; init; }
}
