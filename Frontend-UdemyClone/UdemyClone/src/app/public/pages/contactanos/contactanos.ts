import { Component, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';


@Component({
  selector: 'app-contactanos',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './contactanos.html',
  styleUrl: './contactanos.scss'
})
export class ContactanosComponent {
  private fb = new FormBuilder();
  enviado  = signal(false);
  loading  = signal(false);

  form = this.fb.group({
    nombre:  ['', [Validators.required, Validators.minLength(3)]],
    email:   ['', [Validators.required, Validators.email]],
    asunto:  ['', Validators.required],
    mensaje: ['', [Validators.required, Validators.minLength(10)]]
  });

  onSubmit() {
    if (this.form.invalid) return;
    this.loading.set(true);
    setTimeout(() => {
      this.loading.set(false);
      this.enviado.set(true);
      this.form.reset();
      setTimeout(() => this.enviado.set(false), 5000);
    }, 1200);
  }

  info = [
    { icon: 'fa-solid fa-envelope', label: 'Email',        valor: 'jennyvaque01@gmail.com'    },
    { icon: 'fa-solid fa-location-dot', label: 'Ubicación',    valor: 'Guayaquil, Ecuador'       },
    { icon: 'fa-solid fa-school', label: 'Academia',     valor: 'Tech / Academy'        },
    { icon: 'fa-solid fa-user', label: 'Contacto',     valor: 'Jenny Vaque'           },
  ];

  socials = [
    { icon: 'fa-brands fa-facebook', nombre: 'Facebook',  url: 'https://www.facebook.com'  },
    { icon: 'fa-brands fa-instagram', nombre: 'Instagram', url: 'https://www.instagram.com' },
    { icon: 'fa-brands fa-x-twitter', nombre: 'X (Twitter)', url: 'https://www.x.com'       },
    { icon: 'fa-brands fa-linkedin', nombre: 'LinkedIn',  url: 'https://www.linkedin.com'  },
  ];
}