using UdemyClone.Domain.Entities;

namespace UdemyClone.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> GetAll();
        Task<Categoria?> GetById(int id);
        Task<Categoria> Create(Categoria categoria);
        Task<Categoria?> Update(int id, Categoria datos);
        Task<bool> Delete(int id);
    }
}
