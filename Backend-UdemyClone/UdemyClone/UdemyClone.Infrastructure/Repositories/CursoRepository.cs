using Microsoft.EntityFrameworkCore;
using UdemyClone.Domain.Database;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Infrastructure.Repositories
{
    public class CursoRepository(UdemyCloneContext context) : ICursoRepository
    {
        public async Task<List<Curso>> GetAll()
            => await context.Cursos.Include(c => c.Categoria).ToListAsync();

        public async Task<Curso?> GetById(int id)
            => await context.Cursos.Include(c => c.Categoria)
                .FirstOrDefaultAsync(c => c.CursoId == id);

        public async Task<Curso> Create(Curso curso)
        {
            context.Cursos.Add(curso);
            await context.SaveChangesAsync();
            return curso;
        }

        public async Task<Curso?> Update(int id, Curso datos)
        {
            var curso = await context.Cursos.FindAsync(id);
            if (curso is null) return null;
            curso.Titulo = datos.Titulo;
            curso.Descripcion = datos.Descripcion;
            curso.Precio = datos.Precio;
            curso.Idioma = datos.Idioma;
            curso.Nivel = datos.Nivel;
            curso.CategoriaId = datos.CategoriaId;
            await context.SaveChangesAsync();
            return curso;
        }

        public async Task<bool> Delete(int id)
        {
            var curso = await context.Cursos.FindAsync(id);
            if (curso is null) return false;
            context.Cursos.Remove(curso);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
