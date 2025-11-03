import { inject } from '@angular/core';
import { HttpInterceptorFn } from '@angular/common/http';
import { ServicioAutenticacion } from '@nucleo/services/servicio-autenticacion';

// Interceptor funcional para adjuntar el token JWT
export const interceptorTokenJwt: HttpInterceptorFn = (req, next) => {
  const auth = inject(ServicioAutenticacion);
  const token = auth.getToken();

  if (token && token.trim().length > 0) {
    const reqAutenticada = req.clone({
      setHeaders: { Authorization: `Bearer ${token}` }
    });
    return next(reqAutenticada);
  }

  return next(req);
};
