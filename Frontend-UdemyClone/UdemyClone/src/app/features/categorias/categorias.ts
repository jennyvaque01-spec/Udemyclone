import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CategoriaService } from '../../core/services/categoria.service';
import { CrudBase } from '../../shared/Cruds/crud.base';
import { Categoria } from '../../core/interfaces/categoria.interface';

@Component({
  selector: 'app-categorias',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './categorias.html',
  styleUrl: './categorias.scss'
})
export class CategoriasComponent extends CrudBase<Categoria> implements OnInit {
  private svc = inject(CategoriaService);
  private fb  = inject(FormBuilder);

  form = this.fb.group({
    nombre:          ['', [Validators.required, Validators.maxLength(100)]],
    categoriaPadreId:[null as number | null]
  });

  get filtered() {
    const q = this.searchTerm().toLowerCase();
    return this.items().filter(c => c.nombre.toLowerCase().includes(q));
  }

  get raices() { return this.items().filter(c => !c.categoriaPadreId); }

  ngOnInit() { this.load(); }

  load() {
    this.loading.set(true);
    this.svc.getAll().subscribe({
      next: d => { this.items.set(d); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }

  openEdit(c: Categoria) {
    this.editMode.set(true);
    this.editId = c.categoriaId;
    this.form.patchValue({ nombre: c.nombre, categoriaPadreId: c.categoriaPadreId });
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
      next: () => { this.closeModal(); this.load(); this.showSuccess(this.editMode() ? 'Categoría actualizada ✅' : 'Categoría creada ✅'); this.form.reset(); },
      error: err => this.errorMsg.set(err.error?.mensaje ?? 'Error al guardar')
    });
  }

  doDelete() {
    this.svc.delete(this.deleteId).subscribe({
      next: () => { this.closeConfirm(); this.load(); this.showSuccess('Categoría eliminada ✅'); },
      error: () => this.closeConfirm()
    });
  }
}