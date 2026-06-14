
using Microsoft.EntityFrameworkCore;
using UdemyClone.Domain.Database;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Infrastructure.Repositories
{
    public class ResenaRepository(UdemyCloneContext context) : IResenaRepository
    {
        public async Task<List<Resena>> GetByCurso(int cursoId)
            => await context.Resenas
                .Include(r => r.Estudiante)
                .Include(r => r.Curso)
                .Where(r => r.CursoId == cursoId).ToListAsync();

        public async Task<Resena?> GetById(int estudianteId, int cursoId)
            => await context.Resenas
                .Include(r => r.Estudiante)
                .Include(r => r.Curso)
                .FirstOrDefaultAsync(r => r.EstudianteId == estudianteId && r.CursoId == cursoId);

        public async Task<Resena> Create(Resena resena)
        {
            context.Resenas.Add(resena);
            await context.SaveChangesAsync();
            return resena;
        }

        public async Task<Resena?> Update(int estudianteId, int cursoId, Resena datos)
        {
            var resena = await context.Resenas
                .FirstOrDefaultAsync(r => r.EstudianteId == estudianteId && r.CursoId == cursoId);
            if (resena is null) return null;
            resena.Calificacion = datos.Calificacion;
            resena.ResenaTexto = datos.ResenaTexto;
            await context.SaveChangesAsync();
            return resena;
        }

        public async Task<bool> Delete(int estudianteId, int cursoId)
        {
            var resena = await context.Resenas
                .FirstOrDefaultAsync(r => r.EstudianteId == estudianteId && r.CursoId == cursoId);
            if (resena is null) return false;
            context.Resenas.Remove(resena);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> YaReseno(int estudianteId, int cursoId)
            => await context.Resenas
                .AnyAsync(r => r.EstudianteId == estudianteId && r.CursoId == cursoId);
    }
}
