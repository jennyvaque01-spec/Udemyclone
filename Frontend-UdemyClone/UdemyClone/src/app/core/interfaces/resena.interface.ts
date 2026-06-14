export interface Resena {
  estudianteId: number;
  estudianteNombre: string;
  cursoId: number;
  cursoTitulo: string;
  calificacion: number;
  fecha: string;
  resenaId: number;
  resenaTexto: string | null;
  comentario: string | null;
}

export interface CreateResenaRequest {
  estudianteId: number;
  cursoId: number;
  calificacion: number;
  resenaTexto: string | null;
  comentario: string | null;
}