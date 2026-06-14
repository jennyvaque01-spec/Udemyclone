namespace UdemyClone.Application.Models.DTOs
{
    public class ListaDeseoDto
    {
        public int CursoId { get; set; }
        public string CursoTitulo { get; set; } = null!;
        public decimal CursoPrecio { get; set; }
        public int EstudianteId { get; set; }
    }
}
