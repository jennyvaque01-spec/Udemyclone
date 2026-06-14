using System.ComponentModel.DataAnnotations;

namespace UdemyClone.Application.Models.Requests.Estudiante
{
    public class CreateInstructorRequest
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100)]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Email no válido")]
        public string Email { get; set; } = null!;
    }
}
