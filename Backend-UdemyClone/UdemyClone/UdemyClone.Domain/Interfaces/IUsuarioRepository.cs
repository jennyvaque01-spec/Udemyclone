using UdemyClone.Domain.Entities;

namespace UdemyClone.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByEmail(string email);
        Task<Usuario> Create(Usuario usuario);
        Task<bool> ExisteEmail(string email);
    }
}
