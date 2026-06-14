import { Component, inject, signal } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-private-layout',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './private_layout.html',
  styleUrl: './private_layout.scss'
})
export class PrivateLayoutComponent {
  private auth   = inject(AuthService);
  private router = inject(Router);

  sidebarOpen    = signal(true);
  mobileSidebar  = signal(false);

  navItems = [
    { path: '/admin',               icon: 'fas fa-gauge-high',             label: 'Dashboard',     exact: true  },
    { path: '/admin/estudiantes',   icon: 'fas fa-user-graduate',          label: 'Estudiantes'               },
    { path: '/admin/instructores',  icon: 'fas fa-chalkboard-teacher',     label: 'Instructores'               },
    { path: '/admin/cursos',        icon: 'fas fa-book',                   label: 'Cursos'                     },
    { path: '/admin/categorias',    icon: 'fas fa-tags',                   label: 'Categorías'                 },
    { path: '/admin/inscripciones', icon: 'fas fa-user-check',             label: 'Inscripciones'              },
    { path: '/admin/resenas',       icon: 'fas fa-star',                   label: 'Reseñas'                    },
  ];

  get email()   { return this.auth.getEmail(); }
 get inicial() {
  const email = this.email;
  const parte = email.split('@')[0];       
  const partes = parte.split(/[._-]/);     
  if (partes.length >= 2) {
    return (partes[0][0] + partes[1][0]).toUpperCase(); 
  }
  return partes[0].substring(0, 2).toUpperCase();       
}
  get rol()     { return this.auth.getRol(); }

  toggleSidebar()       { this.sidebarOpen.update(v => !v); }
  toggleMobileSidebar() { this.mobileSidebar.update(v => !v); }

  logout() { this.auth.logout(); }
}