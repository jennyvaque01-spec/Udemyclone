using Microsoft.EntityFrameworkCore;
using UdemyClone.Domain.Database;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Infrastructure.Repositories
{
    public class UsuarioRepository(UdemyCloneContext context) : IUsuarioRepository
    {
        public async Task<Usuario?> GetByEmail(string email)
            => await context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<Usuario> Create(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> ExisteEmail(string email)
            => await context.Usuarios.AnyAsync(u => u.Email == email);
    }
}
