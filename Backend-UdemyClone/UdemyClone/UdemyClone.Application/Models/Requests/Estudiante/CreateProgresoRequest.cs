using System.ComponentModel.DataAnnotations;

namespace UdemyClone.Application.Models.Requests.Estudiante
{
    public class CreateProgresoRequest
    {
        [Required]
        public int EstudianteId { get; set; }

        [Required]
        public int LeccionId { get; set; }
    }
}
