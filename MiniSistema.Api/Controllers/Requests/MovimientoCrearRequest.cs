using System.ComponentModel.DataAnnotations;

namespace MiniSistema.Api.Controllers.Requests;

/// <summary>
/// Solicitud para registrar un movimiento (crear un registro de ajuste) referenciando el producto por nombre.
/// </summary>
public sealed class MovimientoCrearRequest
{
    /// <summary>
    /// Nombre del producto sobre el que se registrará el movimiento.
    /// </summary>
    [Required]
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Cantidad a ajustar (positiva = entrada, negativa = salida).
    /// </summary>
    [Required]
    public int Cantidad { get; set; }
}
