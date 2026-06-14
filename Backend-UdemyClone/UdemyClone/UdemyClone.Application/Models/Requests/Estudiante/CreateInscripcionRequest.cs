using System.ComponentModel.DataAnnotations;

namespace UdemyClone.Application.Models.Requests.Estudiante
{
    public class CreateInscripcionRequest
    {
        [Required]
        public int EstudianteId { get; set; }

        [Required]
        public int CursoId { get; set; }

        [Required]
        public decimal PrecioPagado { get; set; }

        [MaxLength(20)]
        public string? CuponCodigo { get; set; }
    }
}
