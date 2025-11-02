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
        string issuer = _configuration["Jwt:Issuer"] ?? "MiniSistema";
        string audience = _configuration["Jwt:Audience"] ?? issuer;
        string key = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("Clave JWT no configurada (Jwt:Key)");
        int minutes = int.TryParse(_configuration["Jwt:ExpirationMinutes"], out int m) ? m : 60;

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, username),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
        };

        var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(minutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
