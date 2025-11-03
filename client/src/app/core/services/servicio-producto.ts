import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { Producto } from '@modelos/producto.model';
import { MovimientoRequestDto } from '@modelos/movimiento-request-dto.model';


@Injectable({ providedIn: 'root' })
export class ServicioProducto {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl;

  getInventario(): Observable<Producto[]> {
    const url = `${this.baseUrl}/productos/inventario`;
    return this.http.get<Producto[]>(url);
  }

  registrarMovimiento(solicitud: MovimientoRequestDto): Observable<Producto> {
    const url = `${this.baseUrl}/productos/movimiento`;
    return this.http.post<Producto>(url, solicitud);
  }
}
