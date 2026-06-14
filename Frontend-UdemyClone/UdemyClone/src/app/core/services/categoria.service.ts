import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../Environment/environment';
import { CreateCategoriaRequest, Categoria } from '../interfaces/categoria.interface';

@Injectable({ providedIn: 'root' })
export class CategoriaService {
  private http = inject(HttpClient);
  private url = `${environment.apiUrl}/Categorias`;

  getAll() { return this.http.get<Categoria[]>(this.url); }
  getById(id: number) { return this.http.get<Categoria>(`${this.url}/${id}`); }
  create(data: CreateCategoriaRequest) { return this.http.post<Categoria>(this.url, data); }
  update(id: number, data: CreateCategoriaRequest) { return this.http.put<Categoria>(`${this.url}/${id}`, data); }
  delete(id: number) { return this.http.delete(`${this.url}/${id}`); }
}