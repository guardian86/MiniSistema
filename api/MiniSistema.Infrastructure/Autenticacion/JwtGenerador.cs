using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MiniSistema.Application.Interfaces;

namespace MiniSistema.Infrastructure.Autenticacion;

/// <summary>
/// Generador de tokens JWT basado en configuración.
/// </summary>
public sealed class JwtGenerador : IJwtGenerador
{
    private readonly IConfiguration _configuration;

    public JwtGenerador(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public string GenerarToken(string username)
    {
    string emisor = _configuration["JwtConfig:Issuer"]!;
    string audiencia = _configuration["JwtConfig:Audience"]!;
    string key = _configuration["JwtConfig:Key"] ?? throw new InvalidOperationException("Clave JWT no configurada (JwtConfig:Key)");
    int minutos = int.TryParse(_configuration["JwtConfig:ExpirationMinutes"], out int m) ? m : 60;

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, username),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
        };

        var credenciales = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: emisor,
            audience: audiencia,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(minutos),
            signingCredentials: credenciales
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
