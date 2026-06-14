import { Component, HostListener, inject, signal } from '@angular/core';
import { NavigationEnd, RouterLink, RouterLinkActive  } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, CommonModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss'
})
export class NavbarComponent {
   private router = inject(Router);
   
  menuOpen   = signal(false);
  scrolled   = signal(false);
  hideNav = signal(false);

  constructor() {
  this.hideNav.set(this.router.url === '/login');

  this.router.events.pipe(
    filter(e => e instanceof NavigationEnd)
  ).subscribe((e: any) => {
    this.hideNav.set(e.url === '/login');
  });
}

  @HostListener('window:scroll')
  onScroll() {
    this.scrolled.set(window.scrollY > 20);
  }

  toggleMenu() {
    this.menuOpen.update(v => !v);
  }

  closeMenu() {
    this.menuOpen.set(false);
  }
}