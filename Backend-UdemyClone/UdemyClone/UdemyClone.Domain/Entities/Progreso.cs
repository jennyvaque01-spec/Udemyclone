namespace UdemyClone.Domain.Entities
{
    public class Progreso
    {
        public int EstudianteId { get; set; }
        public int LeccionId { get; set; }
        public DateTime FechaTerminada { get; set; } = DateTime.UtcNow;
        public Estudiante Estudiante { get; set; } = null!;
        public Leccion Leccion { get; set; } = null!;
    }
}
