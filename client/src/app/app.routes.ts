import { Routes } from '@angular/router';
import { autenticadoGuardia } from './core/guards/autenticado.guard';

export const routes: Routes = [
	{
		path: 'login',
		loadComponent: () => import('./features/login/pages/login-page/login-page.component').then(m => m.LoginPageComponent)
	},
	{ path: '', pathMatch: 'full', redirectTo: 'inventario' },
	{
		path: 'inventario',
		canActivate: [autenticadoGuardia],
		loadComponent: () => import('./features/inventario/pages/inventario-page/inventario-page.component').then(m => m.InventarioPageComponent)
	},
	{
		path: 'movimiento',
		canActivate: [autenticadoGuardia],
		loadComponent: () => import('./features/movimiento/pages/movimiento-page/movimiento-page.component').then(m => m.MovimientoPageComponent)
	},
	{ path: '**', redirectTo: 'inventario' }
];
