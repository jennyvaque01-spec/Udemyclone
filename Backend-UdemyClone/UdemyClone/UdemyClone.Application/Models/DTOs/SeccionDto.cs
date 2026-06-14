namespace UdemyClone.Application.Models.DTOs
{
    public class SeccionDto
    {
        public int SeccionId { get; set; }
        public int CursoId { get; set; }
        public string Titulo { get; set; } = null!;
        public int Orden { get; set; }
    }
}
