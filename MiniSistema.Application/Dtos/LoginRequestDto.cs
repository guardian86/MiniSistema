namespace MiniSistema.Application.Dtos;

/// <summary>
/// Solicitud de inicio de sesión.
/// </summary>
/// <param name="Username">Nombre de usuario.</param>
/// <param name="Password">Contraseña del usuario.</param>
public readonly record struct LoginRequestDto(string Username, string Password);
