namespace UdemyClone.Application.Models.DTOs
{
    public class LeccionDto
    {
        public int LeccionId { get; set; }
        public int SeccionId { get; set; }
        public string Titulo { get; set; } = null!;
        public int DuracionSegundos { get; set; }
        public int Orden { get; set; }
        public string TipoLeccionNombre { get; set; } = null!;
    }
}
