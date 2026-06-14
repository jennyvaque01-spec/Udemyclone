using System.ComponentModel.DataAnnotations;

namespace UdemyClone.Application.Models.Requests.Estudiante
{
    public class CreateLeccionRequest
    {
        [Required]
        public int SeccionId { get; set; }

        [Required]
        public int TipoLeccionId { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [MaxLength(255)]
        public string Titulo { get; set; } = null!;

        [Required]
        public int DuracionSegundos { get; set; }

        [Required]
        public int Orden { get; set; }
    }
}
