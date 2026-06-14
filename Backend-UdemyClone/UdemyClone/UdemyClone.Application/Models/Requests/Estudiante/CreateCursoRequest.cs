using System.ComponentModel.DataAnnotations;

namespace UdemyClone.Application.Models.Requests.Estudiante
{
    public class CreateCursoRequest
    {
        [Required(ErrorMessage = "El título es obligatorio")]
        [MaxLength(250)]
        public string Titulo { get; set; } = null!;

        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0, 9999.99)]
        public decimal Precio { get; set; }

        public string? Idioma { get; set; }
        public string? Nivel { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria")]
        public int CategoriaId { get; set; }
    }
}
