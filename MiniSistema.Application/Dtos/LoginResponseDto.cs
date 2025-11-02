namespace MiniSistema.Application.Dtos;

/// <summary>
/// Respuesta de inicio de sesión.
/// </summary>
/// <param name="Token">Token JWT emitido para el usuario autenticado.</param>
public readonly record struct LoginResponseDto(string Token);
