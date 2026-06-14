namespace UdemyClone.Domain.Entities
{
    public class CursoInstructor
    {
        public int CursoId { get; set; }
        public int InstructorId { get; set; }

        public Curso Curso { get; set; } = null!;
        public Instructor Instructor { get; set; } = null!;
    }
}
