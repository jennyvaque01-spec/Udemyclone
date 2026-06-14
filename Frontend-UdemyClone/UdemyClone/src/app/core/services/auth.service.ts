import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { LoginRequest, AuthResponse } from '../interfaces/auth.interface';
import { environment } from '../../Environment/environment';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);

  login(data: LoginRequest) {
    return this.http.post<AuthResponse>(`${environment.apiUrl}/auth/login`, data).pipe(
      tap(res => {
        localStorage.setItem('token', res.token);
        localStorage.setItem('email', res.email);
        localStorage.setItem('rol', res.rol);
      })
    );
  }

  logout() { localStorage.clear(); this.router.navigate(['/login']); }
  isLoggedIn(): boolean { return !!localStorage.getItem('token'); }
  getEmail(): string { return localStorage.getItem('email') ?? ''; }
  getRol(): string { return localStorage.getItem('rol') ?? ''; }
}