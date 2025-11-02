using MiniSistema.Application.Dtos;

namespace MiniSistema.Application.Servicios;

/// <summary>
/// Contrato de servicio de aplicación para autenticación de usuarios.
/// </summary>
public interface IAutenticacionServicio
{
    /// <summary>
    /// Realiza el inicio de sesión y devuelve el token JWT si es exitoso.
    /// </summary>
    /// <param name="request">Credenciales de login.</param>
    /// <returns>Respuesta con el token JWT.</returns>
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
}
