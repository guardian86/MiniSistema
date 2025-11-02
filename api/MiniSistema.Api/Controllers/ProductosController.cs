using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniSistema.Application.Dtos;
using MiniSistema.Application.Servicios;


namespace MiniSistema.Api.Controllers;

[ApiController]
[Route("api/productos")]
[Authorize]
public class ProductosController : ControllerBase
{
    private readonly IGestionInventarioServicio _gestionInventario;

    public ProductosController(IGestionInventarioServicio gestionInventario)
    {
        _gestionInventario = gestionInventario ?? throw new ArgumentNullException(nameof(gestionInventario));
    }

    [HttpGet("inventario")]
    [ProducesResponseType(typeof(IEnumerable<ProductoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ConsultarInventario()
    {
        var lista = await _gestionInventario.ConsultarInventarioAsync();
        return Ok(lista);
    }

    [HttpPost("movimiento")]
    [ProducesResponseType(typeof(ProductoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RegistrarMovimiento([FromBody] MovimientoCrearRequestDto request)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.Nombre))
        {
            return BadRequest(new { error = "El nombre del producto es obligatorio." });
        }

        try
        {
            var result = await _gestionInventario.RegistrarMovimientosAsync(request);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    
}
