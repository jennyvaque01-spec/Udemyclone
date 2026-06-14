using Microsoft.EntityFrameworkCore;
using UdemyClone.Domain.Database;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Infrastructure.Repositories
{
    public class ProgresoRepository(UdemyCloneContext context) : IProgresoRepository
    {
        public async Task<List<Progreso>> GetByEstudiante(int estudianteId)
            => await context.Progresos
                .Include(p => p.Leccion)
                .Where(p => p.EstudianteId == estudianteId).ToListAsync();

        public async Task<Progreso> Create(Progreso progreso)
        {
            context.Progresos.Add(progreso);
            await context.SaveChangesAsync();
            return progreso;
        }

        public async Task<bool> Delete(int estudianteId, int leccionId)
        {
            var progreso = await context.Progresos
                .FirstOrDefaultAsync(p => p.EstudianteId == estudianteId && p.LeccionId == leccionId);
            if (progreso is null) return false;
            context.Progresos.Remove(progreso);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Existe(int estudianteId, int leccionId)
            => await context.Progresos
                .AnyAsync(p => p.EstudianteId == estudianteId && p.LeccionId == leccionId);

        public async Task<double> GetPorcentajeAvance(int estudianteId, int cursoId)
        {
            var totalLecciones = await context.Lecciones
                .Where(l => l.Seccion.CursoId == cursoId)
                .CountAsync();

            if (totalLecciones == 0) return 0;

            var completadas = await context.Progresos
                .Where(p => p.EstudianteId == estudianteId
                    && p.Leccion.Seccion.CursoId == cursoId)
                .CountAsync();

            return Math.Round((double)completadas / totalLecciones * 100, 2);
        }
    }
}
