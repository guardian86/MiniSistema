using MiniSistema.Application.Dtos;
using MiniSistema.Application.Interfaces;
using MiniSistema.Application.Servicios;
using MiniSistema.Domain.Interfaces;
using Xunit;

namespace MiniSistema.Tests;

public class AutenticacionServicioTests
{
    private sealed class UsuarioRepositorioFalso : IUsuarioRepositorio
    {
        public Task<bool> ValidarCredencialesAsync(string username, string password)
            => Task.FromResult(username == "admin" && password == "1234");
    }

    private sealed class JwtGeneradorFalso : IJwtGenerador
    {
        public string GenerarToken(string username) => $"token-{username}";
    }

    [Fact]
    public async Task LoginAsync_CredencialesValidas_DeberiaRetornarToken()
    {
        AutenticacionServicio servicio = new(new UsuarioRepositorioFalso(), new JwtGeneradorFalso());
        LoginResponseDto resp = await servicio.LoginAsync(new LoginRequestDto("admin", "1234"));

        Assert.Equal("token-admin", resp.Token);
    }

    [Fact]
    public async Task LoginAsync_CredencialesInvalidas_DeberiaLanzarUnauthorized()
    {
        AutenticacionServicio servicio = new(new UsuarioRepositorioFalso(), new JwtGeneradorFalso());
        await Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => servicio.LoginAsync(new LoginRequestDto("admin", "mala"))
        );
    }
}
