namespace UdemyClone.Application.Models.DTOs
{
    public class InscripcionDto
    {
        public int InscripcionId { get; set; }
        public int EstudianteId { get; set; }
        public string EstudianteNombre { get; set; } = null!;
        public int CursoId { get; set; }
        public string CursoTitulo { get; set; } = null!;
        public DateTime FechaInscripcion { get; set; }
        public decimal PrecioPagado { get; set; }
        public string? CuponCodigo { get; set; }
    }
}
