using UdemyClone.Application.Models.DTOs;
using UdemyClone.Application.Models.Requests.Estudiante;

namespace UdemyClone.Application.Interfaces
{
    public interface ISeccionService
    {
        Task<List<SeccionDto>> GetByCurso(int cursoId);
        Task<SeccionDto?> GetById(int id);
        Task<SeccionDto> Create(CreateSeccionRequest model);
        Task<SeccionDto?> Update(int id, CreateSeccionRequest model);
        Task<bool> Delete(int id);
    }
}
