namespace MiniSistema.Api.Controllers.Requests;

/// <summary>
/// Cuerpo para registrar un movimiento de inventario indicando solo el ajuste.
/// </summary>
public sealed class MovimientoAjusteRequest
{
    /// <summary>
    /// Cantidad a ajustar (positiva para entrada, negativa para salida).
    /// </summary>
    public int CantidadAjuste { get; set; }
}
