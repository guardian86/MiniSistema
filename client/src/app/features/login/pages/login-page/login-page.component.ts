import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ServicioAutenticacion } from '@nucleo/services/servicio-autenticacion';
import { SolicitudLogin } from '@modelos/login-request.model';

@Component({
  selector: 'app-login-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatSnackBarModule, MatProgressBarModule],
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly auth = inject(ServicioAutenticacion);
  private readonly snack = inject(MatSnackBar);

  readonly loginFormulario = this.fb.nonNullable.group({
    username: ['', [Validators.required]],
    password: ['', [Validators.required]]
  });

  cargando = false;

  ngOnInit(): void {
    if (this.auth.isLoggedIn()) {
      this.router.navigate(['/inventario']);
    }
  }

  onSubmit(): void {
    if (this.loginFormulario.invalid) {
      this.loginFormulario.markAllAsTouched();
      return;
    }
    const solicitud: SolicitudLogin = this.loginFormulario.getRawValue();
    this.cargando = true;
    this.auth.login(solicitud).subscribe({
      next: () => {
        this.snack.open('Bienvenido', 'OK', { duration: 2000 });
        this.router.navigate(['/inventario']);
      },
      error: (error) => {
        console.error('Error de autenticación', error);
        this.snack.open('Usuario o contraseña inválidos', 'Cerrar', { duration: 3000 });
      },
      complete: () => { this.cargando = false; }
    });
  }
}
