using UdemyClone.Application.Models.DTOs;
using UdemyClone.Application.Models.Requests.Estudiante;

namespace UdemyClone.Application.Interfaces
{
    public interface ILeccionService
    {
        Task<List<LeccionDto>> GetBySeccion(int seccionId);
        Task<LeccionDto?> GetById(int id);
        Task<LeccionDto> Create(CreateLeccionRequest model);
        Task<LeccionDto?> Update(int id, CreateLeccionRequest model);
        Task<bool> Delete(int id);
    }
}
