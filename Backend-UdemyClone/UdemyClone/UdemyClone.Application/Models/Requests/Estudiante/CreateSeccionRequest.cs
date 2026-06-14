using System.ComponentModel.DataAnnotations;

namespace UdemyClone.Application.Models.Requests.Estudiante
{
    public class CreateSeccionRequest
    {
        [Required]
        public int CursoId { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [MaxLength(255)]
        public string Titulo { get; set; } = null!;

        [Required]
        public int Orden { get; set; }
    }
}
