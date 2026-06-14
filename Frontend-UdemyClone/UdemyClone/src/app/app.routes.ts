import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./public/layout/public.layout').then(m => m.PublicLayoutComponent),
    children: [
      { path: '', loadComponent: () => import('./public/pages/inicio/inicio').then(m => m.InicioComponent) },
      { path: 'quienes-somos', loadComponent: () => import('./public/pages/quienes_somos/quienes.somos').then(m => m.QuienesSomosComponent) },
      { path: 'contactanos', loadComponent: () => import('./public/pages/contactanos/contactanos').then(m => m.ContactanosComponent) },
      { path: 'cursos', loadComponent: () => import('./public/pages/cursos.public/cursos.public').then(m => m.CursosPublicoComponent) },
      { path: 'cursos/:id', loadComponent: () => import('./public/pages/cursos.detalle/cursos.detalle').then(m => m.CursoDetalleComponent) },
    ]
  },
  {
    path: 'login',
    loadComponent: () => import('./features/login/login').then(m => m.LoginComponent)
  },
  {
    path: 'admin',
    canActivate: [authGuard],
    loadComponent: () => import('./features/layout/private_layout').then(m => m.PrivateLayoutComponent),
    children: [
      { path: '', loadComponent: () => import('./features/dashboard/dashboard').then(m => m.DashboardComponent) },
      { path: 'estudiantes', loadComponent: () => import('./features/estudiantes/estudiantes').then(m => m.EstudiantesComponent) },
      { path: 'instructores', loadComponent: () => import('./features/instructores/instructores').then(m => m.InstructoresComponent) },
      { path: 'cursos', loadComponent: () => import('./features/cursos/cursos').then(m => m.CursosComponent) },
      { path: 'categorias', loadComponent: () => import('./features/categorias/categorias').then(m => m.CategoriasComponent) },
      { path: 'inscripciones', loadComponent: () => import('./features/inscripciones/inscripciones').then(m => m.InscripcionesComponent) },
      { path: 'resenas', loadComponent: () => import('./features/resenas/resenas').then(m => m.ResenasComponent) },
    ]
  },

  { path: '**', redirectTo: '' }
];