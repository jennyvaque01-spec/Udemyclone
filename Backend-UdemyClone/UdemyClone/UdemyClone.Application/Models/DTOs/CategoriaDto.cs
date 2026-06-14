namespace UdemyClone.Application.Models.DTOs
{
    public class CategoriaDto
    {
        public int CategoriaId { get; set; }
        public string Nombre { get; set; } = null!;
        public int? CategoriaPadreId { get; set; }
        public string? CategoriaPadreNombre { get; set; }
    }
}
