import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../Environment/environment';
import { CreateResenaRequest, Resena } from '../interfaces/resena.interface';

@Injectable({ providedIn: 'root' })
export class ResenaService {
  private http = inject(HttpClient);
  private url = `${environment.apiUrl}/Resenas`;

  getAll() { 
    return this.http.get<Resena[]>(this.url); 
  }
  getByCurso(cursoId: number) { 
    return this.http.get<Resena[]>(`${this.url}/curso/${cursoId}`); 
  }
  create(data: CreateResenaRequest) { 
    return this.http.post<Resena>(this.url, data); 
  }
  update(resenaId: number, data: Partial<CreateResenaRequest>) { 
    return this.http.put(`${this.url}/${resenaId}`, data); 
  }
  delete(estudianteId: number, cursoId: number) { 
    return this.http.delete(`${this.url}/${estudianteId}/${cursoId}`); 
 }
}