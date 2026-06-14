using UdemyClone.Domain.Entities;

namespace UdemyClone.Domain.Interfaces
{
    public interface IProgresoRepository
    {
        Task<List<Progreso>> GetByEstudiante(int estudianteId);
        Task<Progreso> Create(Progreso progreso);
        Task<bool> Delete(int estudianteId, int leccionId);
        Task<bool> Existe(int estudianteId, int leccionId);
        Task<double> GetPorcentajeAvance(int estudianteId, int cursoId);
    }
}
