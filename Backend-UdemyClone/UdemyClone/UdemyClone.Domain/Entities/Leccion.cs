namespace UdemyClone.Domain.Entities
{
    public class Leccion
    {
        public int LeccionId { get; set; }
        public int SeccionId { get; set; }
        public int TipoLeccionId { get; set; }
        public string Titulo { get; set; } = null!;
        public int DuracionSegundos { get; set; }
        public int Orden { get; set; }
        public Seccion Seccion { get; set; } = null!;

        public TipoLeccion TipoLeccion { get; set; } = null!;
        public ICollection<Progreso> Progresos { get; set; } = [];
    }
}
