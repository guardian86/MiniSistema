using MiniSistema.Domain.Entidades;

namespace MiniSistema.Domain.Interfaces;

/// <summary>
/// Contrato de repositorio para operaciones de lectura y actualización de productos.
/// </summary>
public interface IProductoRepositorio
{
    /// <summary>
    /// Obtiene un producto por su identificador.
    /// </summary>
    /// <param name="id">Identificador del producto.</param>
    /// <returns>El producto encontrado o null si no existe.</returns>
    Task<Producto?> ObtenerPorIdAsync(int id);

    /// <summary>
    /// Obtiene un producto por su nombre exacto (ignorando mayúsculas/minúsculas según proveedor).
    /// </summary>
    Task<Producto?> ObtenerPorNombreAsync(string nombre);

    /// <summary>
    /// Obtiene todos los productos.
    /// </summary>
    /// <returns>Colección de productos. Puede estar vacía pero no es null.</returns>
    Task<IEnumerable<Producto>> ObtenerTodosAsync();

    /// <summary>
    /// Actualiza un producto existente.
    /// </summary>
    /// <param name="producto">Instancia del producto con datos a actualizar.</param>
    Task ActualizarAsync(Producto producto);

    /// <summary>
    /// Crea un nuevo producto y devuelve la entidad con su Id asignado.
    /// </summary>
    Task<Producto> CrearAsync(Producto producto);
}
