using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniSistema.Application.Interfaces;
using MiniSistema.Domain.Interfaces;
using MiniSistema.Infrastructure.Autenticacion;
using MiniSistema.Infrastructure.Persistencia;
using MiniSistema.Infrastructure.Repositorios;

namespace MiniSistema.Infrastructure;

public static class ServicioDependenciasInfraestructura
{
    /// <summary>
    /// Registra DbContext, repositorios y servicios de infraestructura.
    /// </summary>
    public static IServiceCollection AddInfraestructuraServices(this IServiceCollection services, IConfiguration configuration)
    {
        string? conexion = configuration.GetConnectionString("ConexionPostgres");
        if (string.IsNullOrWhiteSpace(conexion))
        {
            throw new InvalidOperationException("Falta ConnectionStrings:ConexionPostgres en la configuración.");
        }

        services.AddDbContext<MiniSistemaDbContext>(options => options.UseNpgsql(conexion));

        // Repositorios e infraestructura
        services.AddScoped<IProductoRepositorio, ProductoRepositorio>();
        services.AddScoped<IUsuarioRepositorio, UsuarioRepositorioEnMemoria>();
        services.AddSingleton<IJwtGenerador, JwtGenerador>();

        return services;
    }
}
