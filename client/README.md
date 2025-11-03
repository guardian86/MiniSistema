# Cliente Angular - MiniSistema de Gestión de Inventario

Proyecto frontend Angular (CLI 20.x) para el MiniSistema de Gestión de Inventario (CCL).

Este README cubre únicamente la ejecución del cliente. Las instrucciones del API están en su propio README.

## Cómo ejecutar (cliente Angular)

Requisitos previos:

- Node.js LTS (18.19+ o 20+ recomendado) y npm
- Angular CLI (opcional para uso global):

```powershell
npm i -g @angular/cli
```

1) Instalar dependencias (una sola vez):

```powershell
npm install
```

2) Configurar el endpoint del API (si aplica):

Edita `src/environments/environment.ts` y confirma `apiUrl`:

```
export const environment = {
	production: false,
	apiUrl: 'https://localhost:5001/api'
};
```

3) Ejecutar en desarrollo (abre el navegador automáticamente):

```powershell
npm start
```

Si el puerto 4200 está ocupado, puedes usar otro puerto (ejemplo 4201):

```powershell
ng serve --port 4201 -o
```

4) Compilar para producción:

```powershell
npm run build
```

El resultado queda en `dist/`.

5) (Opcional) Ejecutar pruebas unitarias:

```powershell
npm test
```

Notas:

- Asegúrate de que el backend (.NET + PostgreSQL) esté ejecutándose y acepte solicitudes desde el origen del frontend (CORS).
- Si cambias la URL del API para producción, recuerda ajustar el archivo de entorno correspondiente.

## Estructura de carpetas (actual)

Este cliente está organizado por features y usa únicamente Pages (pantallas enrutadas con Standalone Components):

```
src/app/
  core/                # guards, interceptors, servicios transversales
  shared/              # layout y utilidades compartidas
  features/
    login/
      pages/
        login-page/
          login-page.component.ts|html|scss
    inventario/
      pages/
        inventario-page/
          inventario-page.component.ts|html|scss
    movimiento/
      pages/
        movimiento-page/
          movimiento-page.component.ts|html|scss
  app.routes.ts        # rutas con loadComponent
  app.config.ts        # providers globales (router, http, interceptors)
```

Notas de diseño:

- Las Pages orquestan datos, navegación y comunicación con servicios.
- Por decisión de simplicidad del proyecto, no se incluyen componentes presentacionales separados; todo el markup vive en cada Page.
- Las rutas usan Standalone Components con `loadComponent` para carga diferida.
