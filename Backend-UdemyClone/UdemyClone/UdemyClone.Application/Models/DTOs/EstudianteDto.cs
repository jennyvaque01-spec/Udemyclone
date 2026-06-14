namespace UdemyClone.Application.Models.DTOs
{
    public class EstudianteDto
    {
        public int EstudianteId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
