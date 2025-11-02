namespace MiniSistema.Application.Dtos;

/// <summary>
/// DTO que representa un producto para transporte de datos hacia/desde la capa de aplicación.
/// </summary>
/// <param name="Id">Identificador del producto.</param>
/// <param name="Nombre">Nombre del producto.</param>
/// <param name="Cantidad">Cantidad actual en inventario.</param>
public readonly record struct ProductoDto(int Id, string Nombre, int Cantidad);
