namespace UdemyClone.Domain.Entities
{
    public class Curso
    {
        public int CursoId { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string? Idioma { get; set; }
        public string? Nivel { get; set; }
        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; } = null!;
        public ICollection<CursoInstructor> CursoInstructores { get; set; } = [];
        public ICollection<Seccion> Secciones { get; set; } = [];
        public ICollection<Inscripcion> Inscripciones { get; set; } = [];
        public ICollection<Resena> Resenas { get; set; } = [];
        public ICollection<ListaDeseo> ListaDeseos { get; set; } = [];
    }
}
