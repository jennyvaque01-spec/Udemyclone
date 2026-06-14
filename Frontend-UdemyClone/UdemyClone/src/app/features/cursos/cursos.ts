import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CursoService } from '../../core/services/curso.service';
import { CategoriaService } from '../../core/services/categoria.service';
import { CrudBase } from '../../shared/Cruds/crud.base';
import { Curso } from '../../core/interfaces/curso.interface';
import {Categoria} from '../../core/interfaces/categoria.interface';

@Component({
  selector: 'app-cursos',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './cursos.html',
  styleUrls: ['./cursos.scss']
})
export class CursosComponent extends CrudBase<Curso> implements OnInit {
  private svc    = inject(CursoService);
  private catSvc = inject(CategoriaService);
  private fb     = inject(FormBuilder);

  categorias: Categoria[] = [];
  niveles = ['Principiante', 'Intermedio', 'Avanzado'];
  idiomas = ['Español', 'Inglés', 'Portugués'];

  form = this.fb.group({
    titulo:      ['', [Validators.required, Validators.maxLength(250)]],
    descripcion: [''],
    precio:      [0,  [Validators.required, Validators.min(0)]],
    idioma:      ['Español'],
    nivel:       ['Principiante'],
    categoriaId: [0,  [Validators.required, Validators.min(1)]]
  });

  get filtered() {
    const q = this.searchTerm().toLowerCase();
    return this.items().filter(c =>
      c.titulo.toLowerCase().includes(q) ||
      c.categoriaNombre.toLowerCase().includes(q)
    );
  }

  nivelColor(n: string | null) {
    if (n === 'Principiante') return '#10B981';
    if (n === 'Intermedio')   return '#F59E0B';
    return '#EF4444';
  }

  ngOnInit() {
    this.load();
    this.catSvc.getAll().subscribe({ next: d => this.categorias = d });
  }

  load() {
    this.loading.set(true);
    this.svc.getAll().subscribe({
      next: d => { this.items.set(d); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  openEdit(c: Curso) {
    this.editMode.set(true);
    this.editId = c.cursoId;
    this.form.patchValue({ titulo: c.titulo, descripcion: c.descripcion, precio: c.precio, idioma: c.idioma, nivel: c.nivel, categoriaId: c.categoriaId });
    this.errorMsg.set('');
    this.showModal.set(true);
  }

  save() {
    if (this.form.invalid) return;
    const data = this.form.value as any;
    const req  = this.editMode() ? this.svc.update(this.editId, data) : this.svc.create(data);
    req.subscribe({
      next: () => { this.closeModal(); this.load(); this.showSuccess(this.editMode() ? 'Curso actualizado ✅' : 'Curso creado ✅'); this.form.reset({ idioma:'Español', nivel:'Principiante', precio:0 }); },
      error: err => this.errorMsg.set(err.error?.mensaje ?? 'Error al guardar')
    });
  }

  doDelete() {
    this.svc.delete(this.deleteId).subscribe({
      next: () => { this.closeConfirm(); this.load(); this.showSuccess('Curso eliminado ✅'); },
      error: () => this.closeConfirm()
    });
  }
}