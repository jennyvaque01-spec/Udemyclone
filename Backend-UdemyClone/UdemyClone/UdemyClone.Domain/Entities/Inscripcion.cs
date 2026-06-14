namespace UdemyClone.Domain.Entities
{
    public class Inscripcion
    {
        public int InscripcionId { get; set; }
        public int EstudianteId { get; set; }
        public int CursoId { get; set; }
        public decimal PrecioPagado { get; set; }
        public DateTime FechaInscripcion { get; set; } = DateTime.UtcNow;
        public string? CuponCodigo { get; set; }
        public Estudiante Estudiante { get; set; } = null!;
        public Curso Curso { get; set; } = null!;
    }
}
