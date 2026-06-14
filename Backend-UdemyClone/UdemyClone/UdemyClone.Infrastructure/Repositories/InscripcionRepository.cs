using Microsoft.EntityFrameworkCore;
using UdemyClone.Domain.Database;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Infrastructure.Repositories
{
    public class InscripcionRepository(UdemyCloneContext context) : IInscripcionRepository
    {
        public async Task<List<Inscripcion>> GetAll()
        {
            return await context.Inscripciones
                .Include(i => i.Estudiante)
                .Include(i => i.Curso)
                .ToListAsync();
        }

        public async Task<List<Inscripcion>> GetByEstudiante(int estudianteId)
        {
            return await context.Inscripciones
                .Include(i => i.Estudiante)
                .Include(i => i.Curso)
                .Where(i => i.EstudianteId == estudianteId)
                .ToListAsync();
        }

        public async Task<Inscripcion?> GetById(int id)
        {
            return await context.Inscripciones
                .Include(i => i.Estudiante)
                .Include(i => i.Curso)
                .FirstOrDefaultAsync(i => i.InscripcionId == id);
        }

        public async Task<Inscripcion> Create(Inscripcion inscripcion)
        {
            context.Inscripciones.Add(inscripcion);
            await context.SaveChangesAsync();
            return inscripcion;
        }

        public async Task<bool> Delete(int id)
        {
            var inscripcion = await context.Inscripciones.FindAsync(id);
            if (inscripcion is null) return false;

            context.Inscripciones.Remove(inscripcion);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> YaInscrito(int estudianteId, int cursoId)
        {
            return await context.Inscripciones
                .AnyAsync(i => i.EstudianteId == estudianteId && i.CursoId == cursoId);
        }
    }
}
