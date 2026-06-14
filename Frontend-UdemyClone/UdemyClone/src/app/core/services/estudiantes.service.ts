import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../Environment/environment';

import { CreateEstudianteRequest, Estudiante } from '../interfaces/estudiante.interface';
@Injectable({ providedIn: 'root' })
export class EstudianteService {
  private http = inject(HttpClient);
  private url = `${environment.apiUrl}/Estudiantes`;

  getAll() { return this.http.get<Estudiante[]>(this.url); }
  getById(id: number) { return this.http.get<Estudiante>(`${this.url}/${id}`); }
  create(data: CreateEstudianteRequest) { return this.http.post<Estudiante>(this.url, data); }
  update(id: number, data: CreateEstudianteRequest) { return this.http.put<Estudiante>(`${this.url}/${id}`, data); }
  delete(id: number) { return this.http.delete(`${this.url}/${id}`); }
}