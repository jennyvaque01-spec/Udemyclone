import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { ResenaService } from '../../core/services/resena.service';
import { CursoService } from '../../core/services/curso.service';
import { EstudianteService } from '../../core/services/estudiantes.service';
import { CrudBase } from '../../shared/Cruds/crud.base';
import { Resena } from '../../core/interfaces/resena.interface';
import { Curso } from '../../core/interfaces/curso.interface';
import { Estudiante } from '../../core/interfaces/estudiante.interface';

@Component({
  selector: 'app-resenas',
  standalone: true,
  imports: [ReactiveFormsModule, DatePipe],
  templateUrl: './resenas.html',
  styleUrl: './resenas.scss'
})
export class ResenasComponent extends CrudBase<Resena> implements OnInit {
  private svc    = inject(ResenaService);
  private curSvc = inject(CursoService);
  private estSvc = inject(EstudianteService);
  private fb     = inject(FormBuilder);

  cursos:      Curso[]      = [];
  estudiantes: Estudiante[] = [];
  deleteEstId = 0;
  deleteCurId = 0;

  form = this.fb.group({
    estudianteId: [0, [Validators.required, Validators.min(1)]],
    cursoId:      [0, [Validators.required, Validators.min(1)]],
    calificacion: [5, [Validators.required, Validators.min(1), Validators.max(5)]],
    resenaTexto:  ['']
  });

  get filtered() {
    const q = this.searchTerm().toLowerCase();
    return this.items().filter(r =>
      r.estudianteNombre.toLowerCase().includes(q) ||
      r.cursoTitulo.toLowerCase().includes(q)
    );
  }

  stars(n: number) { return '★'.repeat(n) + '☆'.repeat(5 - n); }

  ngOnInit() {
    this.curSvc.getAll().subscribe({ next: d => { this.cursos = d; this.load(); } });
    this.estSvc.getAll().subscribe({ next: d => this.estudiantes = d });
  }

  load() {
    this.loading.set(true);
    const all: Resena[] = [];
    let pending = this.cursos.length;
    if (pending === 0) { this.loading.set(false); return; }
    this.cursos.forEach(c => {
      this.svc.getByCurso(c.cursoId).subscribe({
        next: d => { all.push(...d); if (--pending === 0) { this.items.set(all); this.loading.set(false); } },
        error: () => { if (--pending === 0) { this.items.set(all); this.loading.set(false); } }
      });
    });
  }

  confirmDeleteResena(estId: number, curId: number) {
    this.deleteEstId = estId;
    this.deleteCurId = curId;
    this.showConfirm.set(true);
  }

  save() {
    if (this.form.invalid) return;
    this.svc.create(this.form.value as any).subscribe({
      next: () => { this.closeModal(); this.load(); this.showSuccess('Reseña creada ✅'); this.form.reset({ calificacion: 5 }); },
      error: err => this.errorMsg.set(err.error?.mensaje ?? 'Error al crear')
    });
  }

  doDelete() {
    this.svc.delete(this.deleteEstId, this.deleteCurId).subscribe({
      next: () => { this.closeConfirm(); this.load(); this.showSuccess('Reseña eliminada ✅'); },
      error: () => this.closeConfirm()
    });
  }
}