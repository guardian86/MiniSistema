using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MiniSistema.Application.Interfaces;
using MiniSistema.Application.Servicios;
using MiniSistema.Domain.Interfaces;
using MiniSistema.Infrastructure.Autenticacion;
using MiniSistema.Infrastructure.Persistencia;
using MiniSistema.Infrastructure.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// 1) Infraestructura: DbContext (PostgreSQL)
string conexion = builder.Configuration.GetConnectionString("ConexionPostgres")
               ?? builder.Configuration["ConexionPostgres"]
               ?? "Host=localhost;Port=5432;Database=minisistema;Username=postgres;Password=postgres";

builder.Services.AddDbContext<MiniSistemaDbContext>(options =>
    options.UseNpgsql(conexion));

// 2) Repositorios e infraestructura
builder.Services.AddScoped<IProductoRepositorio, ProductoRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorioEnMemoria>();
builder.Services.AddSingleton<IJwtGenerador, JwtGenerador>();

// 3) Servicios de aplicación
builder.Services.AddScoped<IGestionInventarioServicio, GestionInventarioServicio>();
builder.Services.AddScoped<IAutenticacionServicio, AutenticacionServicio>();

// 4) Autenticación y autorización JWT
string jwtKey = builder.Configuration["Jwt:Key"] ?? "21864ea4-ab1f-4141-ac67-d3a69e720497";
string issuer = builder.Configuration["Jwt:Issuer"] ?? "MiniSistema";
string audience = builder.Configuration["Jwt:Audience"] ?? issuer;

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
        };
    });

builder.Services.AddAuthorization();

// 5) Controladores y Swagger con soporte Bearer
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MiniSistema API",
        Version = "v1",
        Description = "API REST para gestión de inventario y autenticación"
    });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Introduce 'Bearer {token}'"
    };
    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new List<string>() }
    });
});

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("PoliticaCors", app =>
    {
        app.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
    });
});

var app = builder.Build();

// Inicialización de base de datos y datos mínimos
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MiniSistemaDbContext>();
    await InicializadorDeDatos.InicializarAsync(db);
}

// Configure Swagger UI accesible en la raíz
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiniSistema API v1");
    c.RoutePrefix = string.Empty;
});

app.UseCors("PoliticaCors");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
