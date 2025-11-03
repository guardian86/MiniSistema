import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { ServicioAutenticacion } from '@nucleo/services/servicio-autenticacion';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, MatToolbarModule, MatButtonModule],
  template: `
    <mat-toolbar color="primary">
      <span class="titulo">MiniSistema de Gestión de Inventario</span>
      <span class="spacer"></span>
      <a mat-button [routerLink]="['/inventario']" routerLinkActive="activo">Inventario</a>
      <a mat-button [routerLink]="['/movimiento']" routerLinkActive="activo">Movimiento</a>
      <button mat-button type="button" (click)="salir()">Salir</button>
    </mat-toolbar>
  `,
  styles: [
    `.spacer{ flex:1 1 auto; }`,
    `.titulo{ font-weight:600; }`,
    `a.activo{ font-weight:600; }`
  ]
})
export class NavbarComponent {
  private readonly auth = inject(ServicioAutenticacion);
  private readonly router = inject(Router);

  salir(): void {
    this.auth.logout();
    this.router.navigate(['/login']);
  }
}
