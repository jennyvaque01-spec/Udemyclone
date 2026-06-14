export interface Instructor {
  instructorId: number;
  nombre: string;
  email: string;
  fechaRegistro: string;
}

export interface CreateInstructorRequest {
  nombre: string;
  email: string;
}
