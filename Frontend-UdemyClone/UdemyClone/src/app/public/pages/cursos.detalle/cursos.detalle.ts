import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CursoService } from '../../../core/services/curso.service';
import { ResenaService } from '../../../core/services/resena.service';
import { Curso } from '../../../core/interfaces/curso.interface';
import { Resena } from '../../../core/interfaces/resena.interface';

@Component({
  selector: 'app-curso-detalle',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './cursos.detalle.html',
  styleUrl: './cursos.detalle.scss'
})
export class CursoDetalleComponent implements OnInit {
  private route  = inject(ActivatedRoute);
  private router = inject(Router);
  private curSvc = inject(CursoService);
  private resSvc = inject(ResenaService);

  curso         = signal<Curso | null>(null);
  resenas       = signal<Resena[]>([]);
  loading       = signal(true);
  inscrito      = signal(false);
  metodoPago    = signal<'gratis' | 'deposito' | null>(null);
  showPagoModal = signal(false);
  showExito     = signal(false);
  progreso      = signal(0);
  seccionAbierta = signal<number | null>(0);

  secciones = [
    {
      titulo: 'Introducción al curso',
      duracion: '45 min',
      lecciones: [
        { titulo: '¿Qué aprenderás en este curso?',   duracion: '5:20',  tipo: 'video',  libre: true,  completada: false },
        { titulo: 'Herramientas necesarias',           duracion: '8:10',  tipo: 'video',  libre: true,  completada: false },
        { titulo: 'Configuración del entorno',         duracion: '12:30', tipo: 'video',  libre: false, completada: false },
        { titulo: 'Recursos del módulo',               duracion: '—',     tipo: 'archivo',libre: false, completada: false },
      ]
    },
    {
      titulo: 'Fundamentos esenciales',
      duracion: '1h 20 min',
      lecciones: [
        { titulo: 'Conceptos básicos y terminología',  duracion: '15:00', tipo: 'video',  libre: false, completada: false },
        { titulo: 'Primer ejercicio práctico',         duracion: '20:45', tipo: 'video',  libre: false, completada: false },
        { titulo: 'Quiz del módulo',                   duracion: '10 min',tipo: 'quiz',   libre: false, completada: false },
        { titulo: 'Tarea práctica',                    duracion: '—',     tipo: 'tarea',  libre: false, completada: false },
      ]
    },
    {
      titulo: 'Desarrollo intermedio',
      duracion: '2h 10 min',
      lecciones: [
        { titulo: 'Técnicas avanzadas',                duracion: '25:00', tipo: 'video',  libre: false, completada: false },
        { titulo: 'Proyecto guiado paso a paso',       duracion: '35:20', tipo: 'video',  libre: false, completada: false },
        { titulo: 'Código fuente del módulo',          duracion: '—',     tipo: 'archivo',libre: false, completada: false },
        { titulo: 'Evaluación intermedia',             duracion: '15 min',tipo: 'quiz',   libre: false, completada: false },
      ]
    },
    {
      titulo: 'Proyecto Final y Certificado',
      duracion: '1h 30 min',
      lecciones: [
        { titulo: 'Diseño del proyecto final',         duracion: '20:00', tipo: 'video',  libre: false, completada: false },
        { titulo: 'Implementación completa',           duracion: '40:00', tipo: 'video',  libre: false, completada: false },
        { titulo: 'Revisión y mejoras',                duracion: '15:00', tipo: 'video',  libre: false, completada: false },
        { titulo: 'Obtén tu certificado',              duracion: '5:00',  tipo: 'video',  libre: false, completada: false },
      ]
    },
  ];

  loQueAprenderas = [
    'Fundamentos sólidos del tema desde cero',
    'Ejercicios prácticos con proyectos reales',
    'Buenas prácticas de la industria',
    'Proyecto final completo y funcional',
    'Certificado oficial al completar',
    'Acceso de por vida al contenido',
    'Soporte directo del instructor',
    'Recursos descargables en cada módulo',
  ];

  requisitos = [
    'Computadora con acceso a internet',
    'Ganas de aprender y practicar',
    'No se requiere experiencia previa',
    'Software gratuito (se indica en el curso)',
  ];

  get totalLecciones() {
    return this.secciones.reduce((a, s) => a + s.lecciones.length, 0);
  }

  get leccionesCompletadas() {
    return this.secciones.reduce((a, s) =>
      a + s.lecciones.filter(l => l.completada).length, 0
    );
  }

  get progresoReal() {
    if (this.totalLecciones === 0) return 0;
    return Math.round((this.leccionesCompletadas / this.totalLecciones) * 100);
  }

  get promedioRating() {
    const r = this.resenas();
    if (!r.length) return '4.8';
    return (r.reduce((a, x) => a + x.calificacion, 0) / r.length).toFixed(1);
  }
 
  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (!id) { this.router.navigate(['/cursos']); return; }

    this.curSvc.getById(id).subscribe({
      next: (c) => {
        this.curso.set(c);
        this.loading.set(false);
      },
      error: () => this.router.navigate(['/cursos'])
    });

    this.resSvc.getByCurso(id).subscribe({
      next: r => this.resenas.set(r),
      error: () => {}
    });

    const saved = JSON.parse(localStorage.getItem(`progreso_${id}`) || '[]');
    if (saved.length) {
      this.secciones.forEach(s =>
        s.lecciones.forEach(l => {
          if (saved.includes(l.titulo)) l.completada = true;
        })
      );
    }

    const inscritos = JSON.parse(localStorage.getItem('cursos_inscritos') || '[]');
    this.inscrito.set(inscritos.includes(id));
  }

  toggleSeccion(i: number) {
    this.seccionAbierta.set(this.seccionAbierta() === i ? null : i);
  }

  toggleLeccion(secIdx: number, lecIdx: number) {
    if (!this.inscrito()) return;
    this.secciones[secIdx].lecciones[lecIdx].completada =
      !this.secciones[secIdx].lecciones[lecIdx].completada;
    this.guardarProgreso();
  }

  guardarProgreso() {
    const id = this.curso()?.cursoId;
    if (!id) return;
    const completadas = this.secciones
      .flatMap(s => s.lecciones)
      .filter(l => l.completada)
      .map(l => l.titulo);
    localStorage.setItem(`progreso_${id}`, JSON.stringify(completadas));
  }

  seleccionarMetodo(m: 'gratis' | 'deposito') {
    this.metodoPago.set(m);
    this.showPagoModal.set(true);
  }

  confirmarInscripcion() {
    const id = this.curso()?.cursoId;
    if (!id) return;
    const inscritos = JSON.parse(localStorage.getItem('cursos_inscritos') || '[]');
    inscritos.push(id);
    localStorage.setItem('cursos_inscritos', JSON.stringify(inscritos));
    this.inscrito.set(true);
    this.showPagoModal.set(false);
    this.showExito.set(true);
    setTimeout(() => this.showExito.set(false), 5000);
  }

  tipoIcon(tipo: string) {
    if (tipo === 'video')   return 'videocam';
    if (tipo === 'quiz')    return 'quiz';
    if (tipo === 'archivo') return 'file';
    if (tipo === 'tarea')   return 'task';
    return '';
  }

  stars(n: number) {
    return ''.repeat(Math.round(n)) + ''.repeat(5 - Math.round(n));
  }

  nivelColor(n: string | null) {
    if (n === 'Principiante') return '#10B981';
    if (n === 'Intermedio')   return '#F59E0B';
    return '#EF4444';
  }
}