using UdemyClone.Domain.Entities;

namespace UdemyClone.Domain.Interfaces
{
    public interface ISeccionRepository
    {
        Task<List<Seccion>> GetByCurso(int cursoId);
        Task<Seccion?> GetById(int id);
        Task<Seccion> Create(Seccion seccion);
        Task<Seccion?> Update(int id, Seccion datos);
        Task<bool> Delete(int id);

    }
}
