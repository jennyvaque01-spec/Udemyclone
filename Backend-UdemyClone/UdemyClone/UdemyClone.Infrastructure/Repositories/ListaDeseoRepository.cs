using Microsoft.EntityFrameworkCore;
using UdemyClone.Domain.Database;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Infrastructure.Repositories
{
    public class ListaDeseoRepository(UdemyCloneContext context) : IListaDeseoRepository
    {
        public async Task<List<ListaDeseo>> GetByEstudiante(int estudianteId)
            => await context.ListaDeseos
                .Include(l => l.Curso)
                .Where(l => l.EstudianteId == estudianteId).ToListAsync();

        public async Task<ListaDeseo> Create(ListaDeseo item)
        {
            context.ListaDeseos.Add(item);
            await context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> Delete(int cursoId, int estudianteId)
        {
            var item = await context.ListaDeseos
                .FirstOrDefaultAsync(l => l.CursoId == cursoId && l.EstudianteId == estudianteId);
            if (item is null) return false;
            context.ListaDeseos.Remove(item);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Existe(int cursoId, int estudianteId)
            => await context.ListaDeseos
                .AnyAsync(l => l.CursoId == cursoId && l.EstudianteId == estudianteId);
    }
}
