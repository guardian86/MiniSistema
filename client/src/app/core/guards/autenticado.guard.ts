import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { ServicioAutenticacion } from '@nucleo/services/servicio-autenticacion';

// Guardia funcional para validar autenticación
export const autenticadoGuardia: CanActivateFn = (_route, state) => {
  const auth = inject(ServicioAutenticacion);
  const router = inject(Router);

  if (auth.isLoggedIn()) {
    return true;
  }

  router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
  return false;
};
