using UdemyClone.Domain.Entities;

namespace UdemyClone.Domain.Interfaces
{
    public interface ILeccionRepository
    {
        Task<List<Leccion>> GetBySeccion(int seccionId);
        Task<Leccion?> GetById(int id);
        Task<Leccion> Create(Leccion leccion);
        Task<Leccion?> Update(int id, Leccion datos);
        Task<bool> Delete(int id);
    }
}
