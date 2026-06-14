using System.ComponentModel.DataAnnotations.Schema;

namespace UdemyClone.Domain.Entities
{
    [Table("Resenas")]
    public class Resena
    {
        public int EstudianteId { get; set; }
        public int CursoId { get; set; }
        public int Calificacion { get; set; }
        public string? ResenaTexto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public Estudiante Estudiante { get; set; } = null!;
        public Curso Curso { get; set; } = null!;
    }
}