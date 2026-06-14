import { Component, OnInit, OnDestroy, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-inicio',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './inicio.html',
  styleUrl: './inicio.scss'
})
export class InicioComponent implements OnInit, OnDestroy {
  private timer: any;
  slide = signal(0);
  anuncioVisible = signal(true);

 slides = [
  {
    titulo:    '¿Eres nuevo en Tech Academy?',
    subtitulo: '¡Estás de suerte!',
    desc:      'Accede a todos los cursos de tecnología completamente gratis. Aprende a tu ritmo.',
    btn:       'Explorar cursos gratis',
    img:       'https://images.unsplash.com/photo-1517694712202-14dd9538aa97?w=600&q=80',
    fallback:  ''
  },
  {
    titulo:    'Aprende habilidades esenciales',
    subtitulo: 'para el trabajo y la vida',
    desc:      'Tech Academy te ayuda a desarrollar las habilidades más demandadas para impulsar tu carrera.',
    btn:       'Ver todos los cursos',
    img:       'https://images.unsplash.com/photo-1498050108023-c5249f4df085?w=600&q=80',
    fallback:  ''
  },
  {
    titulo:    'Instructores certificados',
    subtitulo: '¡Aprende de los mejores!',
    desc:      'Profesionales con experiencia real en la industria te guiarán paso a paso.',
    btn:       'Conocer instructores',
    img:       'https://images.unsplash.com/photo-1531482615713-2afd69097998?w=600&q=80',
    fallback:  ''
  }
];

  habilidades = [
  {
    titulo:  'IA generativa',
    bg:      '#0F172A',
    img:     'https://images.unsplash.com/photo-1677442135703-1787eea5ce01?w=400&q=80',
    fallback: ''
  },
  {
    titulo:  'Certificaciones IT',
    bg:      '#0F2027',
    img:     'https://images.unsplash.com/photo-1550751827-4bd374c3f58b?w=400&q=80',
    fallback: ''
  },
  {
    titulo:  'Desarrollo Web',
    bg:      '#1a0533',
    img:     'https://images.unsplash.com/photo-1547658719-da2b51169166?w=400&q=80',
    fallback: ''
  },
  {
    titulo:  'Base de Datos',
    bg:      '#0a1628',
    img:     'https://images.unsplash.com/photo-1544383835-bda2bc66a55d?w=400&q=80',
    fallback: ''
  }
];
 cursosTendencia = [
  {
    titulo:     'Introducción a Python',
    instructor: 'Carlos Pérez',
    nivel:      'Principiante',
    rating:     4.8,
    reviews:    2341,
    precio:     50,
    icon:       '',
    color:      '#1a1a2e',
    img:        'https://images.unsplash.com/photo-1526374965328-7f61d4dc18c5?w=400&q=80',
    id:         1
  },
  {
    titulo:     'Desarrollo Web con HTML/CSS',
    instructor: 'Ana Sánchez',
    nivel:      'Principiante',
    rating:     4.6,
    reviews:    1823,
    precio:     60,
    icon:       '',
    color:      '#0d3b66',
    img:        'https://images.unsplash.com/photo-1547658719-da2b51169166?w=400&q=80',
    id:         2
  },
  {
    titulo:     'Bases de Datos con MySQL',
    instructor: 'Javier Gómez',
    nivel:      'Intermedio',
    rating:     4.9,
    reviews:    3102,
    precio:     70,
    icon:       '',
    color:      '#0a2e1a',
    img:        'https://images.unsplash.com/photo-1544383835-bda2bc66a55d?w=400&q=80',
    id:         3
  },
  {
    titulo:     'Seguridad Informática',
    instructor: 'Laura Rodríguez',
    nivel:      'Avanzado',
    rating:     4.7,
    reviews:    987,
    precio:     80,
    icon:       '',
    color:      '#2e1a0a',
    img:        'https://images.unsplash.com/photo-1550751827-4bd374c3f58b?w=400&q=80',
    id:         4
  },
  {
    titulo:     'Inteligencia Artificial',
    instructor: 'Eduardo Díaz',
    nivel:      'Intermedio',
    rating:     4.8,
    reviews:    2156,
    precio:     90,
    icon:       '',
    color:      '#1a0533',
    img:        'https://images.unsplash.com/photo-1677442135703-1787eea5ce01?w=400&q=80',
    id:         5
  }
];

stats = [
  {
    num:  '5+',
    icon: '',
    label: 'Cursos disponibles'
  },
  {
    num:  '10+',
    icon: '',
    label: 'Estudiantes activos'
  },
  {
    num:  '5+',
    icon: '',
    label: 'Instructores expertos'
  },
  {
    num:  '100%',
    icon: '',
    label: 'Acceso gratuito'
  }
];
  instructores = [
    { nombre: 'Carlos Pérez',    rol: 'Python & Backend',  inicial: 'C', cursos: 2, estudiantes: 350 },
    { nombre: 'Ana Sánchez',     rol: 'Frontend Web',       inicial: 'A', cursos: 1, estudiantes: 280 },
    { nombre: 'Javier Gómez',    rol: 'Bases de Datos',     inicial: 'J', cursos: 1, estudiantes: 420 },
    { nombre: 'Laura Rodríguez', rol: 'Ciberseguridad',     inicial: 'L', cursos: 1, estudiantes: 190 },
  ];

  ngOnInit() {
    this.timer = setInterval(() =>
      this.slide.update(v => (v + 1) % this.slides.length), 6000);
  }

  ngOnDestroy() { clearInterval(this.timer); }

  prev() { this.slide.update(v => (v - 1 + this.slides.length) % this.slides.length); }
  next() { this.slide.update(v => (v + 1) % this.slides.length); }

  nivelColor(n: string | null) {
    if (n === 'Principiante') return '#7C3AED';
    if (n === 'Intermedio')   return '#EC4899';
    return '#EF4444';
  }

  stars(r: number) { return ''.repeat(Math.round(r)) + ''.repeat(5 - Math.round(r)); }
}