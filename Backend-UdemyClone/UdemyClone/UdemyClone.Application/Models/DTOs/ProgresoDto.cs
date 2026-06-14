namespace UdemyClone.Application.Models.DTOs
{
    public class ProgresoDto
    {
        public int EstudianteId { get; set; }
        public int LeccionId { get; set; }
        public string LeccionTitulo { get; set; } = null!;
        public DateTime FechaTerminada { get; set; }
    }
}
