namespace UdemyClone.Domain.Entities
{
    public class Seccion
    {
        public int SeccionId { get; set; }
        public int CursoId { get; set; }
        public string Titulo { get; set; } = null!;
        public int Orden { get; set; }
        public Curso Curso { get; set; } = null!;
        public ICollection<Leccion> Lecciones { get; set; } = [];
    }
}
