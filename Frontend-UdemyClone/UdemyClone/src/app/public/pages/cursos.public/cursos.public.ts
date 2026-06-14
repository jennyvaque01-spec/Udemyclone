import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { CursoService } from '../../../core/services/curso.service';
import { CategoriaService } from '../../../core/services/categoria.service';
import { Curso } from '../../../core/interfaces/curso.interface';
import { Categoria } from '../../../core/interfaces/categoria.interface';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-cursos-publico',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './cursos.public.html',
  styleUrl: './cursos.public.scss'
})
export class CursosPublicoComponent implements OnInit {
  private curSvc = inject(CursoService);
  private catSvc = inject(CategoriaService);
  private router = inject(Router);

  cursos:     Curso[]     = [];
  categorias: Categoria[] = [];
  loading     = true;
  filtroCategoria = 0;
  filtroNivel     = '';
  searchTerm      = '';

  niveles = ['', 'Principiante', 'Intermedio', 'Avanzado'];

  get cursosFiltrados() {
    return this.cursos.filter(c => {
      const matchCat   = this.filtroCategoria === 0 || c.categoriaId === this.filtroCategoria;
      const matchNivel = !this.filtroNivel    || c.nivel === this.filtroNivel;
      const matchSearch = !this.searchTerm   ||
        c.titulo.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        c.categoriaNombre.toLowerCase().includes(this.searchTerm.toLowerCase());
      return matchCat && matchNivel && matchSearch;
    });
  }

  nivelColor(nivel: string | null) {
    if (nivel === 'Principiante') return '#10B981';
    if (nivel === 'Intermedio')   return '#F59E0B';
    return '#EF4444';
  }

  getThumbBg(categoriaId: number): string {
    const colores: { [key: number]: string } = {
      1: '#7C3AED', 
      2: '#EC4899', 
      3: '#3B82F6', 
      4: '#10B981'
    };
    return colores[categoriaId] || '#64748B';
  }
  getIconoPorCategoria(categoriaId: number): string {
  const iconos: { [key: number]: string } = {
    1: 'code',           
    2: 'web',            
    3: 'storage',        
    4: 'psychology',     
    5: 'security'        
  };
  return iconos[categoriaId] || 'school'; 
}
  ngOnInit() {
    this.curSvc.getAll().subscribe({
      next: d => { this.cursos = d; this.loading = false; },
      error: () => { this.loading = false; }
    });
    this.catSvc.getAll().subscribe({
      next: d => this.categorias = d
    });
  }

  verDetalle(id: number) {
    this.router.navigate(['/cursos', id]);
  }
}