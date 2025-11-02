namespace MiniSistema.Application.Interfaces;

/// <summary>
/// Contrato para la generación de tokens JWT.
/// </summary>
public interface IJwtGenerador
{
    /// <summary>
    /// Genera un token JWT para el usuario indicado.
    /// </summary>
    /// <param name="username">Nombre de usuario.</param>
    /// <returns>Token JWT firmado en formato string.</returns>
    string GenerarToken(string username);
}
