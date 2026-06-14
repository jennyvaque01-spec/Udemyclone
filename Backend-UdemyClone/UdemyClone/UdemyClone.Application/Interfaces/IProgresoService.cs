using UdemyClone.Application.Models.DTOs;
using UdemyClone.Application.Models.Requests.Estudiante;

namespace UdemyClone.Application.Interfaces
{
    public interface IProgresoService
    {
        Task<List<ProgresoDto>> GetByEstudiante(int estudianteId);
        Task<ProgresoDto> Create(CreateProgresoRequest model);
        Task<bool> Delete(int estudianteId, int leccionId);
        Task<double> GetPorcentajeAvance(int estudianteId, int cursoId);
    }
}
