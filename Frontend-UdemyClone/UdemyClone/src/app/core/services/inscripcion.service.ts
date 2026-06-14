import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../Environment/environment';
import { CreateInscripcionRequest, Inscripcion } from '../interfaces/inscripcion.interface';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({ providedIn: 'root' })
export class InscripcionService {
  private http = inject(HttpClient);
  private url = `${environment.apiUrl}/Inscripciones`;


  getAll(): Observable<Inscripcion[]> {
    return this.http.get<Inscripcion[]>(this.url);
  }
  getByEstudiante(id: number) { 
    return this.http.get<Inscripcion[]>(`${this.url}/estudiante/${id}`); 
  }
  getById(id: number) { 
    return this.http.get<Inscripcion>(`${this.url}/${id}`); 
  }
  create(data: CreateInscripcionRequest) { 
    return this.http.post<Inscripcion>(this.url, data); 
  }
  update(id: number, data: any) {
    return this.http.put(`${this.url}/${id}`, data); 
  }
  delete(id: number) { 
    return this.http.delete(`${this.url}/${id}`); 
  }
}