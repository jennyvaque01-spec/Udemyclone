namespace UdemyClone.Application.Models.DTOs
{
    public class CursoDto
    {
        public int CursoId { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string? Idioma { get; set; }
        public string? Nivel { get; set; }
        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; } = null!;
    }
}
