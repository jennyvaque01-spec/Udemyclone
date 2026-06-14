namespace UdemyClone.Domain.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Rol { get; set; } = "Estudiante";
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}
