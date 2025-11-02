namespace MiniSistema.Domain.Interfaces;

/// <summary>
/// Contrato de repositorio para validación de credenciales de usuario.
/// </summary>
public interface IUsuarioRepositorio
{
    /// <summary>
    /// Valida las credenciales del usuario.
    /// </summary>
    /// <param name="username">Nombre de usuario.</param>
    /// <param name="password">Contraseña del usuario.</param>
    /// <returns>true si las credenciales son válidas; en caso contrario, false.</returns>
    Task<bool> ValidarCredencialesAsync(string username, string password);
}
