import { Component} from '@angular/core';
import { RouterLink } from '@angular/router';
@Component({
  selector: 'app-quienes-somos',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './quienes.somos.html',
  styleUrl: './quienes.somos.scss'
})
export class QuienesSomosComponent {
  valores = [
    { icon: '', titulo: 'Innovación',     desc: 'Usamos las últimas tecnologías para ofrecer la mejor experiencia educativa.' },
    { icon: '', titulo: 'Colaboración',   desc: 'Creemos en el aprendizaje colectivo y el apoyo mutuo entre estudiantes.' },
    { icon: '', titulo: 'Excelencia',     desc: 'Nos comprometemos con la calidad en cada curso y lección.' },
    { icon: '', titulo: 'Accesibilidad',  desc: 'El conocimiento debe ser accesible para todos, sin barreras.' },
  ];

  stack = [
    { nombre: '.NET 9',            color: '#7C3AED', desc: 'Framework backend principal'        },
    { nombre: 'Angular 21',        color: '#EC4899', desc: 'Framework frontend moderno'         },
    { nombre: 'SQL Server',        color: '#38BDF8', desc: 'Base de datos relacional'           },
    { nombre: 'Entity Framework',  color: '#10B981', desc: 'ORM para mapeo objeto-relacional'   },
    { nombre: 'JWT Auth',          color: '#F59E0B', desc: 'Autenticación stateless segura'     },
    { nombre: 'BCrypt',            color: '#A855F7', desc: 'Encriptación de contraseñas'        },
    { nombre: 'Serilog',           color: '#EC4899', desc: 'Logging estructurado'               },
    { nombre: 'Scalar',            color: '#38BDF8', desc: 'Documentación moderna de la API'    },
    { nombre: 'Mailjet',           color: '#10B981', desc: 'Envío de correos en producción'     },
    { nombre: 'Docker',            color: '#3B82F6', desc: 'Contenedores para SQL Server'       },
  ];

  timeline = [
    { fase: '01', titulo: 'Diseño de BD',      desc: 'Modelado de 12 tablas relacionales con SQL Server', icon: ''  },
    { fase: '02', titulo: 'Backend .NET 9',    desc: 'API REST con arquitectura limpia por capas',         icon: ''  },
    { fase: '03', titulo: 'Seguridad JWT',     desc: 'Autenticación, BCrypt y User Secrets',               icon: ''  },
    { fase: '04', titulo: 'Emails & Logs',     desc: 'Serilog + Mailjet + SMTP4Dev',                       icon: ''  },
    { fase: '05', titulo: 'Frontend Angular',  desc: 'UI responsive con Angular 21',                       icon: ''  },
  ];
}