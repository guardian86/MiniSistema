namespace MiniSistema.Domain.Entidades;

/// <summary>
/// Entidad del dominio que representa un producto en inventario.
/// </summary>
public class Producto
{
    /// <summary>
    /// Identificador único del producto.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre del producto.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Cantidad disponible en inventario.
    /// </summary>
    public int Cantidad { get; set; }
}
