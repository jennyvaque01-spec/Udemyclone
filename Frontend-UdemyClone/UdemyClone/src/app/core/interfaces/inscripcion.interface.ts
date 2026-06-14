export interface Inscripcion {
  inscripcionId: number;
  estudianteId: number;
  estudianteNombre: string;
  cursoId: number;
  cursoTitulo: string;
  fechaInscripcion: string;
  precioPagado: number;
  cuponCodigo: string | null;
  estado: 'Activa' | 'Completada' | 'Cancelada';
}

export interface CreateInscripcionRequest {
  estudianteId: number;
  cursoId: number;
  precioPagado: number;
  cuponCodigo: string | null;
}
