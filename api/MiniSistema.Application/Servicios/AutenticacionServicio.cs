using MiniSistema.Application.Dtos;
using MiniSistema.Application.Interfaces;
using MiniSistema.Domain.Interfaces;

namespace MiniSistema.Application.Servicios;

/// <summary>
/// Implementación del servicio de autenticación.
/// </summary>
public sealed class AutenticacionServicio : IAutenticacionServicio
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly IJwtGenerador _jwtGenerador;

    public AutenticacionServicio(IUsuarioRepositorio usuarioRepositorio, IJwtGenerador jwtGenerador)
    {
        _usuarioRepositorio = usuarioRepositorio ?? throw new ArgumentNullException(nameof(usuarioRepositorio));
        _jwtGenerador = jwtGenerador ?? throw new ArgumentNullException(nameof(jwtGenerador));
    }

    /// <inheritdoc />
    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        bool valido = await _usuarioRepositorio.ValidarCredencialesAsync(request.Username, request.Password).ConfigureAwait(false);
        if (!valido)
        {
            throw new UnauthorizedAccessException("Credenciales inválidas.");
        }

        string token = _jwtGenerador.GenerarToken(request.Username);
        return new LoginResponseDto(token);
    }
}
