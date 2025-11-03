import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from 'environments/environment';
import { SolicitudLogin } from '@modelos/login-request.model';
import { RespuestaLogin } from '@modelos/login-response.model';

@Injectable({ providedIn: 'root' })
export class ServicioAutenticacion {
  private readonly claveToken = 'token';
  private readonly http = inject(HttpClient);

  login(solicitud: SolicitudLogin): Observable<RespuestaLogin> {
    const url = `${environment.apiUrl}/auth/login`;
    return this.http.post<RespuestaLogin>(url, solicitud).pipe(
      tap((respuesta) => this.guardarToken(respuesta.token))
    );
  }

  logout(): void {
    try {
      localStorage.removeItem(this.claveToken);
    } catch {
      // Ignorar errores de almacenamiento
    }
  }

  getToken(): string | null {
    try {
      return localStorage.getItem(this.claveToken);
    } catch {
      return null;
    }
  }

  isLoggedIn(): boolean {
    const token = this.getToken();
    return !!token && token.trim().length > 0;
  }

  private guardarToken(token: string): void {
    try {
      localStorage.setItem(this.claveToken, token);
    } catch {
      // Ignorar errores de almacenamiento
    }
  }
}
