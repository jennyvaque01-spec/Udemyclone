using UdemyClone.Domain.Entities;

namespace UdemyClone.Domain.Interfaces
{
    public interface IInstructorRepository
    {
        Task<List<Instructor>> GetAll();
        Task<Instructor?> GetById(int id);
        Task<Instructor> Create(Instructor instructor);
        Task<Instructor?> Update(int id, Instructor datos);
        Task<bool> Delete(int id);
        Task<bool> ExisteEmail(string email);
    }
}
