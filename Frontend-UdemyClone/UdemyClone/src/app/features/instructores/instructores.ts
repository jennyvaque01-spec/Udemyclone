import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { InstructorService } from '../../core/services/instructor.service';
import { CrudBase } from '../../shared/Cruds/crud.base';
import { Instructor } from '../../core/interfaces/instructor.interface';

@Component({
  selector: 'app-instructores',
  standalone: true,
  imports: [ReactiveFormsModule, DatePipe],
  templateUrl: './instructores.html',
  styleUrl: './instructores.scss'
})
export class InstructoresComponent extends CrudBase<Instructor> implements OnInit {
  private svc = inject(InstructorService);
  private fb  = inject(FormBuilder);

  form = this.fb.group({
    nombre: ['', [Validators.required, Validators.maxLength(100)]],
    email:  ['', [Validators.required, Validators.email]]
  });

  get filtered() {
    const q = this.searchTerm().toLowerCase();
    return this.items().filter(i =>
      i.nombre.toLowerCase().includes(q) ||
      i.email.toLowerCase().includes(q)
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

  openEdit(i: Instructor) {
    this.editMode.set(true);
    this.editId = i.instructorId;
    this.form.patchValue({ nombre: i.nombre, email: i.email });
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
      next: () => { this.closeModal(); this.load(); this.showSuccess(this.editMode() ? 'Instructor actualizado ✅' : 'Instructor creado ✅'); this.form.reset(); },
      error: err => this.errorMsg.set(err.error?.mensaje ?? 'Error al guardar')
    });
  }

  doDelete() {
    this.svc.delete(this.deleteId).subscribe({
      next: () => { this.closeConfirm(); this.load(); this.showSuccess('Instructor eliminado ✅'); },
      error: () => this.closeConfirm()
    });
  }
}