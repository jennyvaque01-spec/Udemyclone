import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../Environment/environment';
import { CreateInstructorRequest, Instructor } from '../interfaces/instructor.interface';

@Injectable({ providedIn: 'root' })
export class InstructorService {
  private http = inject(HttpClient);
  private url = `${environment.apiUrl}/Instructores`;

  getAll() { return this.http.get<Instructor[]>(this.url); }
  getById(id: number) { return this.http.get<Instructor>(`${this.url}/${id}`); }
  create(data: CreateInstructorRequest) { return this.http.post<Instructor>(this.url, data); }
  update(id: number, data: CreateInstructorRequest) { return this.http.put<Instructor>(`${this.url}/${id}`, data); }
  delete(id: number) { return this.http.delete(`${this.url}/${id}`); }
}