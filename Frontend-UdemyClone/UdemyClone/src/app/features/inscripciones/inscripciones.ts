import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { DatePipe, DecimalPipe } from '@angular/common';
import { InscripcionService } from '../../core/services/inscripcion.service';
import { EstudianteService } from '../../core/services/estudiantes.service';
import { CursoService } from '../../core/services/curso.service';
import { CrudBase } from '../../shared/Cruds/crud.base';
import { Inscripcion } from '../../core/interfaces/inscripcion.interface';
import { Estudiante } from '../../core/interfaces/estudiante.interface';
import { Curso } from '../../core/interfaces/curso.interface';

@Component({
  selector: 'app-inscripciones',
  standalone: true,
  imports: [ReactiveFormsModule, DatePipe, DecimalPipe],
  templateUrl: './inscripciones.html',
  styleUrl: './inscripciones.scss'
})
export class InscripcionesComponent extends CrudBase<Inscripcion> implements OnInit {
  private svc    = inject(InscripcionService);
  private estSvc = inject(EstudianteService);
  private curSvc = inject(CursoService);
  private fb     = inject(FormBuilder);

  estudiantes: Estudiante[] = [];
  cursos:      Curso[]      = [];

  form = this.fb.group({
    estudianteId: [0,  [Validators.required, Validators.min(1)]],
    cursoId:      [0,  [Validators.required, Validators.min(1)]],
    precioPagado: [0,  [Validators.required, Validators.min(0)]],
    cuponCodigo:  ['']
  });

  get filtered() {
    const q = this.searchTerm().toLowerCase();
    return this.items().filter(i =>
      i.estudianteNombre.toLowerCase().includes(q) ||
      i.cursoTitulo.toLowerCase().includes(q)
    );
  }

  ngOnInit() {
    this.load(); 
    this.estSvc.getAll().subscribe({ next: d => this.estudiantes = d });
    this.curSvc.getAll().subscribe({ next: d => this.cursos = d });
  }

  load() {
    this.loading.set(true);
    this.svc.getAll().subscribe({
      next: d => { 
        this.items.set(d); 
        this.loading.set(false); 
      },
      error: () => this.loading.set(false)
    });
  }

  save() {
    if (this.form.invalid) return;
    this.svc.create(this.form.value as any).subscribe({
      next: () => { 
        this.closeModal(); 
        this.load(); 
        this.showSuccess('Inscripción creada '); 
        this.form.reset({ precioPagado: 0 }); 
      },
      error: err => this.errorMsg.set(err.error?.mensaje ?? 'Error al crear')
    });
  }

  doDelete() {
    this.svc.delete(this.deleteId).subscribe({
      next: () => { 
        this.closeConfirm(); 
        this.load(); 
        this.showSuccess('Inscripción eliminada '); 
      },
      error: () => this.closeConfirm()
    });
  }
}
