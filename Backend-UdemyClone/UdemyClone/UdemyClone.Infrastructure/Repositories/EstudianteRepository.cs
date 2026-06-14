using Microsoft.EntityFrameworkCore;
using UdemyClone.Domain.Database;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Infrastructure.Repositories
{
    public class EstudianteRepository(UdemyCloneContext context) : IEstudianteRepository
    {
        public async Task<List<Estudiante>> GetAll()
        {
            return await context.Estudiantes.ToListAsync();
        }

        public async Task<Estudiante?> GetById(int id)
        {
            return await context.Estudiantes.FindAsync(id);
        }

        public async Task<Estudiante> Create(Estudiante estudiante)
        {
            context.Estudiantes.Add(estudiante);
            await context.SaveChangesAsync();
            return estudiante;
        }

        public async Task<Estudiante?> Update(int id, Estudiante datos)
        {
            var estudiante = await context.Estudiantes.FindAsync(id);
            if (estudiante is null) return null;

            estudiante.Nombre = datos.Nombre;
            await context.SaveChangesAsync();
            return estudiante;
        }

        public async Task<bool> Delete(int id)
        {
            var estudiante = await context.Estudiantes.FindAsync(id);
            if (estudiante is null) return false;

            context.Estudiantes.Remove(estudiante);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExisteEmail(string email)
        {
            return await context.Estudiantes.AnyAsync(e => e.Email == email);
        }
    }
}
