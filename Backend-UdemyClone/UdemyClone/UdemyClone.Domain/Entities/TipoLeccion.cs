namespace UdemyClone.Domain.Entities
{
    public class TipoLeccion
    {

        public int TipoLeccionId { get; set; }
        public string Nombre { get; set; } = null!;
        public ICollection<Leccion> Lecciones { get; set; } = [];
    }
}
