using UdemyClone.Domain.Entities;

namespace UdemyClone.Domain.Interfaces
{
    public interface IEstudianteRepository
    {
        Task<List<Estudiante>> GetAll();
        Task<Estudiante?> GetById(int id);
        Task<Estudiante> Create(Estudiante estudiante);
        Task<Estudiante?> Update(int id, Estudiante estudiante);
        Task<bool> Delete(int id);
        Task<bool> ExisteEmail(string email);
    }
}
