namespace UdemyClone.Domain.Entities
{
    public class ListaDeseo
    {
        public int CursoId { get; set; }
        public int EstudianteId { get; set; }
        public Curso Curso { get; set; } = null!;
        public Estudiante Estudiante { get; set; } = null!;
    }
}
