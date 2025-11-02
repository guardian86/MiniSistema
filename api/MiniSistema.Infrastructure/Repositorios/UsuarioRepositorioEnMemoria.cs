using Microsoft.Extensions.Configuration;
using MiniSistema.Domain.Interfaces;

namespace MiniSistema.Infrastructure.Repositorios;

/// <summary>
/// Repositorio de usuarios en memoria con credenciales fijas.
/// </summary>
public sealed class UsuarioRepositorioEnMemoria : IUsuarioRepositorio
{
    private readonly string _user;
    private readonly string _pass;

    public UsuarioRepositorioEnMemoria(IConfiguration configuration)
    {
        _user = configuration["Autenticacion:Usuario"]!;
        _pass = configuration["Autenticacion:Password"]!;
    }

    public Task<bool> ValidarCredencialesAsync(string username, string password)
        => Task.FromResult(string.Equals(username, _user, StringComparison.Ordinal) && string.Equals(password, _pass, StringComparison.Ordinal));
}
