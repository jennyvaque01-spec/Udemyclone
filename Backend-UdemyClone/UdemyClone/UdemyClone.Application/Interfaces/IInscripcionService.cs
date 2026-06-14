using UdemyClone.Application.Models.DTOs;
using UdemyClone.Application.Models.Requests.Estudiante;

namespace UdemyClone.Application.Interfaces
{
    public interface IInscripcionService
    {
        Task<List<InscripcionDto>> GetAll();
        Task<List<InscripcionDto>> GetByEstudiante(int estudianteId);
        Task<InscripcionDto?> GetById(int id);
        Task<InscripcionDto> Create(CreateInscripcionRequest model);
        Task<bool> Delete(int id);
    }
}
