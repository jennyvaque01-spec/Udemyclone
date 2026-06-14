using Microsoft.EntityFrameworkCore;
using UdemyClone.Domain.Database;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Infrastructure.Repositories
{
    public class LeccionRepository(UdemyCloneContext context) : ILeccionRepository
    {
        public async Task<List<Leccion>> GetBySeccion(int seccionId)
            => await context.Lecciones.Include(l => l.TipoLeccion)
                .Where(l => l.SeccionId == seccionId)
                .OrderBy(l => l.Orden).ToListAsync();

        public async Task<Leccion?> GetById(int id)
            => await context.Lecciones.Include(l => l.TipoLeccion)
                .FirstOrDefaultAsync(l => l.LeccionId == id);

        public async Task<Leccion> Create(Leccion leccion)
        {
            context.Lecciones.Add(leccion);
            await context.SaveChangesAsync();
            return leccion;
        }

        public async Task<Leccion?> Update(int id, Leccion datos)
        {
            var leccion = await context.Lecciones.FindAsync(id);
            if (leccion is null) return null;
            leccion.Titulo = datos.Titulo;
            leccion.DuracionSegundos = datos.DuracionSegundos;
            leccion.Orden = datos.Orden;
            leccion.TipoLeccionId = datos.TipoLeccionId;
            await context.SaveChangesAsync();
            return leccion;
        }

        public async Task<bool> Delete(int id)
        {
            var leccion = await context.Lecciones.FindAsync(id);
            if (leccion is null) return false;
            context.Lecciones.Remove(leccion);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
