export interface Curso {
  cursoId: number;
  titulo: string;
  descripcion: string | null;
  precio: number;
  idioma: string | null;
  nivel: string | null;
  categoriaId: number;
  categoriaNombre: string;
}

export interface CreateCursoRequest {
  titulo: string;
  descripcion: string | null;
  precio: number;
  idioma: string | null;
  nivel: string | null;
  categoriaId: number;
}