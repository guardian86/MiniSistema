import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { ServicioProducto } from '@nucleo/services/servicio-producto';
import { Producto } from '@modelos/producto.model';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-inventario-page',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatTableModule, MatSnackBarModule, MatProgressBarModule],
  templateUrl: './inventario-page.component.html',
  styleUrls: ['./inventario-page.component.scss']
})
export class InventarioPageComponent implements OnInit {

  private readonly servicio = inject(ServicioProducto);
  private readonly snack = inject(MatSnackBar);
  productos: Producto[] = [];
  readonly columnas = ['id','nombre','cantidad'];
  cargando = false;

  ngOnInit(): void {
    this.cargando = true;
    this.servicio.getInventario().subscribe({
      next: (data) => this.productos = data,
      error: (e) => {
        console.error('Error al cargar inventario', e);
        this.snack.open('Error al cargar inventario', 'Cerrar', { duration: 3000 });
      },
      complete: () => { this.cargando = false; }
    });
  }

  
}
