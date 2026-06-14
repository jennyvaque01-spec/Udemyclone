using Microsoft.EntityFrameworkCore;
using UdemyClone.Domain.Database;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Infrastructure.Repositories
{
    public class InstructorRepository(UdemyCloneContext context) : IInstructorRepository
    {
        public async Task<List<Instructor>> GetAll()
            => await context.Instructores.ToListAsync();

        public async Task<Instructor?> GetById(int id)
            => await context.Instructores.FindAsync(id);

        public async Task<Instructor> Create(Instructor instructor)
        {
            context.Instructores.Add(instructor);
            await context.SaveChangesAsync();
            return instructor;
        }

        public async Task<Instructor?> Update(int id, Instructor datos)
        {
            var instructor = await context.Instructores.FindAsync(id);
            if (instructor is null) return null;
            instructor.Nombre = datos.Nombre;
            instructor.Email = datos.Email;
            await context.SaveChangesAsync();
            return instructor;
        }

        public async Task<bool> Delete(int id)
        {
            var instructor = await context.Instructores.FindAsync(id);
            if (instructor is null) return false;
            context.Instructores.Remove(instructor);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExisteEmail(string email)
            => await context.Instructores.AnyAsync(i => i.Email == email);
    }
}
