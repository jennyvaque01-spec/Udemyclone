using System.ComponentModel.DataAnnotations.Schema;

namespace UdemyClone.Domain.Entities
{
    [Table("Estudiantes")]
    public class Estudiante
    {
        public int EstudianteId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public ICollection<Inscripcion> Inscripciones { get; set; } = [];
        public ICollection<Resena> Resenas { get; set; } = [];
        public ICollection<Progreso> Progresos { get; set; } = [];
    }
}
