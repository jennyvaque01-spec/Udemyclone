import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../Environment/environment';
import { CreateCursoRequest, Curso } from '../interfaces/curso.interface';

@Injectable({ providedIn: 'root' })
export class CursoService {
  private http = inject(HttpClient);
  private url = `${environment.apiUrl}/Cursos`;

  getAll() { return this.http.get<Curso[]>(this.url); }
  getById(id: number) { return this.http.get<Curso>(`${this.url}/${id}`); }
  create(data: CreateCursoRequest) { return this.http.post<Curso>(this.url, data); }
  update(id: number, data: CreateCursoRequest) { return this.http.put<Curso>(`${this.url}/${id}`, data); }
  delete(id: number) { return this.http.delete(`${this.url}/${id}`); }
}