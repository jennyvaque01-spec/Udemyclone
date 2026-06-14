export interface Estudiante {
  estudianteId: number;
  nombre: string;
  email: string;
  fechaRegistro: string;
}

export interface CreateEstudianteRequest {
  nombre: string;
  email: string;
}