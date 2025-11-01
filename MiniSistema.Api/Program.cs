var builder = WebApplication.CreateBuilder(args);

// Servicios de la API
builder.Services.AddControllers();
// OpenAPI (Swagger)
builder.Services.AddOpenApi();

var app = builder.Build();

// Middleware y pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
