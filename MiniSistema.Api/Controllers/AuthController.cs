using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniSistema.Application.Dtos;
using MiniSistema.Application.Servicios;

namespace MiniSistema.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAutenticacionServicio _autenticacionServicio;

    public AuthController(IAutenticacionServicio autenticacionServicio)
    {
        _autenticacionServicio = autenticacionServicio ?? throw new ArgumentNullException(nameof(autenticacionServicio));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        try
        {
            var respuesta = await _autenticacionServicio.LoginAsync(request);
            return Ok(respuesta);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }
}
