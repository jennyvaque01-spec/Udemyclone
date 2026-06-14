namespace UdemyClone.Application.Models.DTOs
{
    public class ResenaDto
    {
        public int EstudianteId { get; set; }
        public string EstudianteNombre { get; set; } = null!;
        public int CursoId { get; set; }
        public string CursoTitulo { get; set; } = null!;
        public int Calificacion { get; set; }
        public string? ResenaTexto { get; set; }
        public DateTime Fecha { get; set; }
    }
}
