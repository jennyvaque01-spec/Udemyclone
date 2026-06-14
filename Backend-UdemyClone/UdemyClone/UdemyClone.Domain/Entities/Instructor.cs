using System.ComponentModel.DataAnnotations.Schema;

namespace UdemyClone.Domain.Entities
{
    [Table("Instructores")]
    public class Instructor
    {
        public int InstructorId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public ICollection<CursoInstructor> CursoInstructores { get; set; } = [];
    }
}
