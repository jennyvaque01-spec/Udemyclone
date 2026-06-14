using UdemyClone.Domain.Entities;

namespace UdemyClone.Domain.Interfaces
{
    public interface IListaDeseoRepository
    {
        Task<List<ListaDeseo>> GetByEstudiante(int estudianteId);
        Task<ListaDeseo> Create(ListaDeseo item);
        Task<bool> Delete(int cursoId, int estudianteId);
        Task<bool> Existe(int cursoId, int estudianteId);
    }
}
