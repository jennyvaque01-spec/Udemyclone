namespace UdemyClone.Domain.Entities
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        public string Nombre { get; set; } = null!;
        public int? CategoriaPadreId { get; set; }

        public Categoria? CategoriaPadre { get; set; }
        public ICollection<Categoria> Subcategorias { get; set; } = [];
        public ICollection<Curso> Cursos { get; set; } = [];
    }
}
