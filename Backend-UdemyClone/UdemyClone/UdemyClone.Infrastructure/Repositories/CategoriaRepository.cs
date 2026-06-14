using Microsoft.EntityFrameworkCore;
using UdemyClone.Domain.Database;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Infrastructure.Repositories
{
    public class CategoriaRepository(UdemyCloneContext context) : ICategoriaRepository
    {
        public async Task<List<Categoria>> GetAll()
            => await context.Categorias.Include(c => c.CategoriaPadre).ToListAsync();

        public async Task<Categoria?> GetById(int id)
            => await context.Categorias.Include(c => c.CategoriaPadre)
                .FirstOrDefaultAsync(c => c.CategoriaId == id);

        public async Task<Categoria> Create(Categoria categoria)
        {
            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categoria?> Update(int id, Categoria datos)
        {
            var categoria = await context.Categorias.FindAsync(id);
            if (categoria is null) return null;
            categoria.Nombre = datos.Nombre;
            categoria.CategoriaPadreId = datos.CategoriaPadreId;
            await context.SaveChangesAsync();
            return categoria;
        }

        public async Task<bool> Delete(int id)
        {
            var categoria = await context.Categorias.FindAsync(id);
            if (categoria is null) return false;
            context.Categorias.Remove(categoria);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
