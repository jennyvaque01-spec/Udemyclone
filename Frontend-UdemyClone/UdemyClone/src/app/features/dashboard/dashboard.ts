import { Component, inject, OnInit, signal, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { RouterLink } from '@angular/router';
import { InstructorService } from '../../core/services/instructor.service';
import { EstudianteService } from '../../core/services/estudiantes.service';
import { CursoService } from '../../core/services/curso.service';
import { AuthService } from '../../core/services/auth.service';
import { InscripcionService } from '../../core/services/inscripcion.service';
import { Chart, registerables } from 'chart.js';

Chart.register(...registerables);

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class DashboardComponent implements OnInit, AfterViewInit {
  @ViewChild('barChart') barChartRef!: ElementRef<HTMLCanvasElement>;

  private estSvc   = inject(EstudianteService);
  private instSvc  = inject(InstructorService);
  private curSvc   = inject(CursoService);
  private auth     = inject(AuthService);
  private inscSvc  = inject(InscripcionService);

  loading = signal(true);

  stats = signal([
    { label: 'Menú',         value: 0, icon: 'fas fa-bars',               color: '#38BDF8', path: '/admin/menu'         },
    { label: 'Estudiantes',  value: 0, icon: 'fas fa-user-graduate',       color: '#7C3AED', path: '/admin/estudiantes'  },
    { label: 'Instructores', value: 0, icon: 'fas fa-chalkboard-teacher',  color: '#EC4899', path: '/admin/instructores' },
    { label: 'Cursos',       value: 0, icon: 'fas fa-book',                color: '#38BDF8', path: '/admin/cursos'       },
    { label: 'Módulos',      value: 4, icon: 'fas fa-layer-group',         color: '#10B981', path: '/admin/categorias'   },
  ]);

  quickLinks = [
    { icon: 'fas fa-user-graduate',      label: 'Estudiantes',   path: '/admin/estudiantes'   },
    { icon: 'fas fa-chalkboard-teacher', label: 'Instructores',  path: '/admin/instructores'  },
    { icon: 'fas fa-book',               label: 'Cursos',        path: '/admin/cursos'        },
    { icon: 'fas fa-cubes',              label: 'Categorías',    path: '/admin/categorias'    },
    { icon: 'fas fa-user-check',         label: 'Inscripciones', path: '/admin/inscripciones' },
    { icon: 'fas fa-star',               label: 'Reseñas',       path: '/admin/resenas'       },
  ];

  systemInfo = [
    { icon: 'fas fa-server',       title: 'Backend',   desc: '.NET 9 + Entity Framework Core 9 + SQL Server' },
    { icon: 'fas fa-puzzle-piece', title: 'Frontend',  desc: 'Angular 21 + SCSS + Standalone Components'     },
    { icon: 'fas fa-shield-alt',   title: 'Seguridad', desc: 'JWT + BCrypt + User Secrets + Middlewares'     },
    { icon: 'fas fa-list-alt',     title: 'Logging',   desc: 'Serilog + SQL Server Sink + Scalar Docs'       },
  ];

  recentActivity = [
    { id: 1, icon: 'fas fa-user-plus',          color: '#7C3AED', title: 'Nuevo estudiante registrado', sub: 'maria.lopez@email.com',    time: 'Hace 5 min'  },
    { id: 2, icon: 'fas fa-book-open',          color: '#38BDF8', title: 'Curso publicado',             sub: 'Angular desde cero',       time: 'Hace 18 min' },
    { id: 3, icon: 'fas fa-star',               color: '#F59E0B', title: 'Nueva reseña 5 estrellas',    sub: 'React Avanzado',           time: 'Hace 32 min' },
    { id: 4, icon: 'fas fa-user-check',         color: '#10B981', title: 'Inscripción completada',      sub: 'carlos.p → Node.js Pro',   time: 'Hace 1 h'    },
    { id: 5, icon: 'fas fa-chalkboard-teacher', color: '#EC4899', title: 'Instructor verificado',       sub: 'prof.ana@techacademy.com', time: 'Hace 2 h'    },
    { id: 6, icon: 'fas fa-book',               color: '#38BDF8', title: 'Nuevo curso creado',          sub: 'Vue 3 + Composition API',  time: 'Hace 3 h'    },
  ];

  get hora() {
    const h = new Date().getHours();
    return h < 12 ? 'Buenos días' : h < 18 ? 'Buenas tardes' : 'Buenas noches';
  }
  get email() { return this.auth.getEmail(); }

  ngOnInit()       { this.loadStats(); }
  ngAfterViewInit(){ this.loadChartData(); }

  loadStats() {
    let done = 0;
    const check = () => { if (++done === 3) this.loading.set(false); };

    this.estSvc.getAll().subscribe({
      next: d => { this.stats.update(s => { s[1].value = d.length; return [...s]; }); check(); },
      error: check
    });
    this.instSvc.getAll().subscribe({
      next: d => { this.stats.update(s => { s[2].value = d.length; return [...s]; }); check(); },
      error: check
    });
    this.curSvc.getAll().subscribe({
      next: d => { this.stats.update(s => { s[3].value = d.length; return [...s]; }); check(); },
      error: check
    });
  }

  loadChartData() {
    this.inscSvc.getAll().subscribe({
      next: (inscripciones) => {
        const porMes = new Array(12).fill(0);
        inscripciones.forEach(i => {
          const mes = new Date(i.fechaInscripcion).getMonth(); // 0 = Enero
          porMes[mes]++;
        });
        this.buildBarChart(porMes);
      },
      error: () => this.buildBarChart(new Array(12).fill(0))
    });
  }

  buildBarChart(porMes: number[]) {
    const ctx = this.barChartRef?.nativeElement;
    if (!ctx) return;

    new Chart(ctx, {
      type: 'bar',
      data: {
        labels: ['Ene','Feb','Mar','Abr','May','Jun','Jul','Ago','Sep','Oct','Nov','Dic'],
        datasets: [{
          label: 'Inscripciones reales',
          data: porMes,
          backgroundColor: 'rgba(124,58,237,0.75)',
          borderColor: '#7C3AED',
          borderWidth: 1,
          borderRadius: 6,
        }]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
          legend: { labels: { color: '#94A3B8', font: { size: 12 } } },
          tooltip: {
            backgroundColor: '#1E293B',
            titleColor: '#F8FAFC',
            bodyColor: '#94A3B8',
            callbacks: {
              label: ctx => ` ${ctx.parsed.y} inscripciones`
            }
          }
        },
        scales: {
          x: { ticks: { color: '#64748B' }, grid: { color: 'rgba(255,255,255,0.04)' } },
          y: {
            ticks: { color: '#64748B', stepSize: 1 },
            grid: { color: 'rgba(255,255,255,0.06)' },
            beginAtZero: true
          }
        }
      }
    });
  }
}