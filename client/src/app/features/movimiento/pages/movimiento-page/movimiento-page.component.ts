import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { Router } from '@angular/router';
import { ServicioProducto } from '@nucleo/services/servicio-producto';
import { Producto } from '@modelos/producto.model';
import { MovimientoRequestDto } from '@modelos/movimiento-request-dto.model';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-movimiento-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatCardModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatButtonModule, MatSnackBarModule, MatProgressBarModule],
  templateUrl: './movimiento-page.component.html',
  styleUrls: ['./movimiento-page.component.scss']
})
export class MovimientoPageComponent implements OnInit {

  private readonly fb = inject(FormBuilder);
  private readonly servicio = inject(ServicioProducto);
  private readonly router = inject(Router);
  private readonly snack = inject(MatSnackBar);

  productos: Producto[] = [];

  readonly formulario = this.fb.nonNullable.group({
    nombre: ['', [Validators.required]],
    cantidad: [0, [Validators.required]]
  });

  cargando = false;

  ngOnInit(): void {
    this.servicio.getInventario().subscribe({
      next: (data) => this.productos = data,
      error: (e) => console.error('Error al cargar productos', e)
    });
  }

  onSubmit(): void {
    if (this.formulario.invalid) {
      this.formulario.markAllAsTouched();
      return;
    }
    const solicitud: MovimientoRequestDto = this.formulario.getRawValue();
    this.cargando = true;
    this.servicio.registrarMovimiento(solicitud).subscribe({
      next: () => {
        this.snack.open('Movimiento registrado', 'OK', { duration: 2000 });
        this.router.navigate(['/inventario']);
      },
      error: (e) => {
        console.error('Error al registrar movimiento', e);
        this.snack.open('Error al registrar movimiento', 'Cerrar', { duration: 3000 });
      },
      complete: () => { this.cargando = false; }
    });
  }

  
}
