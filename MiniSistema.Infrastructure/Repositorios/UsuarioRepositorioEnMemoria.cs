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
        // Lee credenciales desde configuración (appsettings: Autenticacion:Usuario/Password)
        _user = configuration["Autenticacion:Usuario"] ?? "admin";
        _pass = configuration["Autenticacion:Password"] ?? "1234";
    }

    public Task<bool> ValidarCredencialesAsync(string username, string password)
        => Task.FromResult(string.Equals(username, _user, StringComparison.Ordinal) && string.Equals(password, _pass, StringComparison.Ordinal));
}
