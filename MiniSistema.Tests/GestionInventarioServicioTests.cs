using MiniSistema.Application.Dtos;
using MiniSistema.Application.Servicios;
using MiniSistema.Domain.Entidades;
using MiniSistema.Domain.Interfaces;
using Xunit;

namespace MiniSistema.Tests;

public class GestionInventarioServicioTests
{
    private sealed class ProductoRepositorioFalso : IProductoRepositorio
    {
        private readonly Dictionary<int, Producto> _db = new()
        {
            [1] = new Producto { Id = 1, Nombre = "Lapicero", Cantidad = 10 },
            [2] = new Producto { Id = 2, Nombre = "Cuaderno", Cantidad = 5 }
        };
        private int _nextId = 3;

        public Task ActualizarAsync(Producto producto)
        {
            _db[producto.Id] = producto;
            return Task.CompletedTask;
        }

        public Task<Producto?> ObtenerPorIdAsync(int id)
        {
            _db.TryGetValue(id, out var p);
            return Task.FromResult<Producto?>(p);
        }

        public Task<Producto?> ObtenerPorNombreAsync(string nombre)
        {
            var p = _db.Values.FirstOrDefault(x => string.Equals(x.Nombre, nombre, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult<Producto?>(p);
        }

        public Task<IEnumerable<Producto>> ObtenerTodosAsync()
            => Task.FromResult<IEnumerable<Producto>>(_db.Values.ToList());

        public Task<Producto> CrearAsync(Producto producto)
        {
            producto.Id = _nextId++;
            _db[producto.Id] = producto;
            return Task.FromResult(producto);
        }
    }

    [Fact]
    public async Task ConsultarInventarioAsync_DeberiaRetornarProductosMapeados()
    {
        ProductoRepositorioFalso repo = new();
        GestionInventarioServicio servicio = new(repo);

        IEnumerable<ProductoDto> resultado = await servicio.ConsultarInventarioAsync();

        Assert.Collection(resultado,
            p => { Assert.Equal(1, p.Id); Assert.Equal("Lapicero", p.Nombre); Assert.Equal(10, p.Cantidad); },
            p => { Assert.Equal(2, p.Id); Assert.Equal("Cuaderno", p.Nombre); Assert.Equal(5, p.Cantidad); }
        );
    }

    [Fact]
    public async Task RegistrarMovimientoAsync_DeberiaActualizarCantidadYDevolverDto()
    {
        ProductoRepositorioFalso repo = new();
        GestionInventarioServicio servicio = new(repo);

        ProductoDto dto = await servicio.RegistrarMovimientoAsync(new MovimientoRequestDto(1, 3));

        Assert.Equal(1, dto.Id);
        Assert.Equal("Lapicero", dto.Nombre);
        Assert.Equal(13, dto.Cantidad);
    }

    [Fact]
    public async Task RegistrarMovimientoAsync_ProductoInexistente_DeberiaLanzarExcepcion()
    {
        ProductoRepositorioFalso repo = new();
        GestionInventarioServicio servicio = new(repo);

        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => servicio.RegistrarMovimientoAsync(new MovimientoRequestDto(99, 1))
        );
    }
}
