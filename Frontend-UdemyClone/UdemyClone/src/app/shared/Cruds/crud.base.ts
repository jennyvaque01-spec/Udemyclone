import { signal } from '@angular/core';

export abstract class CrudBase<T> {
  items    = signal<T[]>([]);
  loading  = signal(true);
  showModal   = signal(false);
  showConfirm = signal(false);
  editMode    = signal(false);
  successMsg  = signal('');
  errorMsg    = signal('');
  searchTerm  = signal('');
  editId      = 0;
  deleteId    = 0;

  abstract load(): void;

  openCreate() {
    this.editMode.set(false);
    this.editId = 0;
    this.errorMsg.set('');
    this.showModal.set(true);
  }

  closeModal()   { this.showModal.set(false);   this.errorMsg.set(''); }
  closeConfirm() { this.showConfirm.set(false); }

  confirmDelete(id: number) {
    this.deleteId = id;
    this.showConfirm.set(true);
  }

  showSuccess(msg: string) {
    this.successMsg.set(msg);
    setTimeout(() => this.successMsg.set(''), 3000);
  }
}