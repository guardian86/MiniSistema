# MiniSistema – API de Inventario (local)

Guía rápida para ejecutar en local la API del MiniSistema de Gestión de Inventario.

## Requisitos

- .NET SDK 9.0
- PostgreSQL 14+ en la máquina de ejecución
- PowerShell (Windows) o tu terminal preferida

## Proyectos y capas

- `MiniSistema.Api`: Punto de entrada (Web API + Swagger)
- `MiniSistema.Application`: Servicios de aplicación y DTOs (reglas de negocio)
- `MiniSistema.Domain`: Entidades y contratos (repositorios)
- `MiniSistema.Infrastructure`: EF Core (PostgreSQL), repositorios y JWT

## Configuración

1) Base de datos en PostgreSQL DB - db_minisistema:

```powershell
|# Usuario - postgres | Password - admin /host/
psql -U postgres -h localhost -p 5432 -c "CREATE DATABASE db_minisistema;"
```

2) Configurar `appsettings.json` en `MiniSistema.Api`:

```json
{
	"Logging": {
		"LogLevel": { "Default": "Information", "Microsoft.AspNetCore": "Warning" }
	},
	"Autenticacion": { "Usuario": "admin", "Password": "1234" },
	"JwtConfig": {
		"Key": "21864ea4-ab1f-4141-ac67-d3a69e720497",
		"Issuer": "MiniSistema",
		"Audience": "MiniSistema",
		"ExpirationMinutes": 120
	},
	"ConnectionStrings": {
		"ConexionPostgres": "Host=localhost;Port=5432;Database=db_minisistema;Username=postgres;Password=admin"
	}
}
```

- La API usa `ConnectionStrings:ConexionPostgres`.
- Las credenciales de login (en memoria) salen de `Autenticacion`.
- La configuración de JWT está bajo `JwtConfig`.

## Compilar y ejecutar

Desde la carpeta raíz del repo (`MiniSistema`):

```powershell
# Restaurar y compilar
dotnet restore
dotnet build

# Ejecutar solo la API (HTTPS 5001 / HTTP 5000)
dotnet run --project .\MiniSistema.Api\MiniSistema.Api.csproj

# (Opcional) Forzar otros puertos
# dotnet run --project .\MiniSistema.Api\MiniSistema.Api.csproj --urls "http://localhost:5123;https://localhost:5124"
```

La API expone Swagger en la raíz:

- https://localhost:5001/
- http://localhost:5000/

En el primer arranque se crean datos mínimos si la tabla está vacía: `Lapicero (10)` y `Cuaderno (5)`.

## Flujo de prueba (Swagger)

1) Autenticación (público)

- POST `/api/auth/login`
- Body (JSON):

```json
{ "usuario": "admin", "password": "1234" }
```

2) Autorizar en Swagger

- Pulsa "Authorize" y pega: `Bearer <tu_token>`

3) Registrar un movimiento (entrada/salida)

- POST `/api/productos/movimiento`
- Body (JSON):

```json
{ "nombre": "Café", "cantidad": 10 }
```

- Si el producto no existe y la cantidad es positiva, se crea con esa cantidad.
- Si no existe y la cantidad es negativa/cero, devuelve 404.

4) Consultar inventario

- GET `/api/productos/inventario`

## Endpoints mínimos

- POST `/api/auth/login` (anónimo): autenticación; devuelve JWT.
- POST `/api/productos/movimiento` (protegido): registra entrada/salida por `nombre` y `cantidad`.
- GET `/api/productos/inventario` (protegido): listado de productos.

## Solución de problemas

- Certificado HTTPS desarrollo:

```powershell
dotnet dev-certs https --check
dotnet dev-certs https --trust
```

- Error de puertos “forbidden by its access permissions”:
	- Cambia puertos en `MiniSistema.Api/Properties/launchSettings.json`, o ejecuta con `--urls` (ver arriba).
	- O libera el puerto:

```powershell
netstat -ano | Select-String ":5001"
# Toma el PID y luego:
taskkill /PID <pid> /F
```

- Error “Host can't be null” (Npgsql):
	- Asegúrate de definir `ConnectionStrings:ConexionPostgres` en `appsettings.json`.

## Notas de seguridad

- Por defecto, todos los endpoints requieren JWT salvo `/api/auth/login` (se aplica una FallbackPolicy global).
- Cambia `JwtConfig:Key` en producción y guarda el secreto de forma segura.

## Tests (opcional)

```powershell
dotnet test
```

## Estructura (resumen)

```
MiniSistema.Api/              # Web API + Swagger
MiniSistema.Application/      # Servicios de aplicación y DTOs
MiniSistema.Domain/           # Entidades y contratos
MiniSistema.Infrastructure/   # EF Core (PostgreSQL), repositorios, JWT
```

---

