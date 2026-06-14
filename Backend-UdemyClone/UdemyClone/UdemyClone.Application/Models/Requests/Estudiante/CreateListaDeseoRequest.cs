using System.ComponentModel.DataAnnotations;

namespace UdemyClone.Application.Models.Requests.Estudiante
{
    public class CreateListaDeseoRequest
    {
        [Required]
        public int CursoId { get; set; }

        [Required]
        public int EstudianteId { get; set; }
    }
}
