using UdemyClone.Domain.Entities;

namespace UdemyClone.Domain.Interfaces
{
    public interface IResenaRepository
    {
        Task<List<Resena>> GetByCurso(int cursoId);
        Task<Resena?> GetById(int estudianteId, int cursoId);
        Task<Resena> Create(Resena resena);
        Task<Resena?> Update(int estudianteId, int cursoId, Resena datos);
        Task<bool> Delete(int estudianteId, int cursoId);
        Task<bool> YaReseno(int estudianteId, int cursoId);
    }
}
