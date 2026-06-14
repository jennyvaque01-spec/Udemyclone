using System.ComponentModel.DataAnnotations;

namespace UdemyClone.Application.Models.Requests.Estudiante
{
    public class CreateResenaRequest
    {

        [Required]
        public int EstudianteId { get; set; }

        [Required]
        public int CursoId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "La calificación debe ser entre 1 y 5")]
        public int Calificacion { get; set; }

        public string? ResenaTexto { get; set; }
    }
}
