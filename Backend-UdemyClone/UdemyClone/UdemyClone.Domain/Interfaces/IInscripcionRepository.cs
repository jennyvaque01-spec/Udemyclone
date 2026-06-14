using UdemyClone.Domain.Entities;

namespace UdemyClone.Domain.Interfaces
{
    public interface IInscripcionRepository
    {
        Task<List<Inscripcion>> GetAll();
        Task<List<Inscripcion>> GetByEstudiante(int estudianteId);
        Task<Inscripcion?> GetById(int id);
        Task<Inscripcion> Create(Inscripcion inscripcion);
        Task<bool> Delete(int id);
        Task<bool> YaInscrito(int estudianteId, int cursoId);
    }
}
