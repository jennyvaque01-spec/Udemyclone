import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class LoginComponent {
  private fb     = inject(FormBuilder);
  private auth   = inject(AuthService);
  private router = inject(Router);

  loading      = signal(false);
  showPass     = signal(false);
  errorMsg     = signal('');

features = [
  { icon: 'school', text: 'Gestión completa de cursos'    },
  { icon: 'group', text: 'Control de estudiantes'      },
  { icon: 'stadium', text: 'Seguimiento de progreso'        },
  { icon: 'security', text: 'Acceso seguro con JWT'          },
];


  form = this.fb.group({
    email:    ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  onSubmit() {
    if (this.form.invalid) return;
    this.loading.set(true);
    this.errorMsg.set('');

    this.auth.login(this.form.value as any).subscribe({
      next: () => this.router.navigate(['/admin']),
      error: (err) => {
        this.loading.set(false);
        this.errorMsg.set(err.error?.mensaje ?? 'Email o contraseña incorrectos');
      }
    });
  }
}