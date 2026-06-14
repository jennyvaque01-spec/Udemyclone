import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { EstudianteService } from '../../core/services/estudiantes.service';
import { CrudBase } from '../../shared/Cruds/crud.base';
import { Estudiante } from '../../core/interfaces/estudiante.interface';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-estudiantes',
  standalone: true,
  imports: [ReactiveFormsModule, DatePipe],
  templateUrl: './estudiantes.html',
  styleUrl: './estudiantes.scss'
})
export class EstudiantesComponent extends CrudBase<Estudiante> implements OnInit {
  private svc = inject(EstudianteService);
  private fb  = inject(FormBuilder);

  form = this.fb.group({
    nombre: ['', [Validators.required, Validators.maxLength(100)]],
    email:  ['', [Validators.required, Validators.email]]
  });

  get filtered() {
    const q = this.searchTerm().toLowerCase();
    return this.items().filter(e =>
      e.nombre.toLowerCase().includes(q) ||
      e.email.toLowerCase().includes(q)
    );
  }

  ngOnInit() { this.load(); }

  load() {
    this.loading.set(true);
    this.svc.getAll().subscribe({
      next: d => { this.items.set(d); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  openEdit(e: Estudiante) {
    this.editMode.set(true);
    this.editId = e.estudianteId;
    this.form.patchValue({ nombre: e.nombre, email: e.email });
    this.errorMsg.set('');
    this.showModal.set(true);
  }

  save() {
    if (this.form.invalid) return;
    const data = this.form.value as any;
    const req  = this.editMode()
      ? this.svc.update(this.editId, data)
      : this.svc.create(data);

    req.subscribe({
      next: () => {
        this.closeModal();
        this.load();
        this.showSuccess(this.editMode() ? 'Estudiante actualizado ✅' : 'Estudiante creado ✅');
        this.form.reset();
      },
      error: err => this.errorMsg.set(err.error?.mensaje ?? 'Error al guardar')
    });
  }

  doDelete() {
    this.svc.delete(this.deleteId).subscribe({
      next: () => { this.closeConfirm(); this.load(); this.showSuccess('Estudiante eliminado ✅'); },
      error: () => this.closeConfirm()
    });
  }
}