using Microsoft.EntityFrameworkCore;
using UdemyClone.Domain.Database;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Infrastructure.Repositories
{
    public class SeccionRepository(UdemyCloneContext context) : ISeccionRepository
    {
        public async Task<List<Seccion>> GetByCurso(int cursoId)
            => await context.Secciones.Where(s => s.CursoId == cursoId)
                .OrderBy(s => s.Orden).ToListAsync();

        public async Task<Seccion?> GetById(int id)
            => await context.Secciones.FindAsync(id);

        public async Task<Seccion> Create(Seccion seccion)
        {
            context.Secciones.Add(seccion);
            await context.SaveChangesAsync();
            return seccion;
        }

        public async Task<Seccion?> Update(int id, Seccion datos)
        {
            var seccion = await context.Secciones.FindAsync(id);
            if (seccion is null) return null;
            seccion.Titulo = datos.Titulo;
            seccion.Orden = datos.Orden;
            await context.SaveChangesAsync();
            return seccion;
        }

        public async Task<bool> Delete(int id)
        {
            var seccion = await context.Secciones.FindAsync(id);
            if (seccion is null) return false;
            context.Secciones.Remove(seccion);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
