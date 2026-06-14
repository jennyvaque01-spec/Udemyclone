using UdemyClone.Domain.Entities;

namespace UdemyClone.Domain.Interfaces
{
    public interface ICursoRepository
    {
        Task<List<Curso>> GetAll();
        Task<Curso?> GetById(int id);
        Task<Curso> Create(Curso curso);
        Task<Curso?> Update(int id, Curso datos);
        Task<bool> Delete(int id);
    }
}
