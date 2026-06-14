using System.ComponentModel.DataAnnotations;

namespace UdemyClone.Application.Models.Requests.Estudiante
{
    public class UpdateEstudianteRequest
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Nombre { get; set; } = null!;
    }
}
