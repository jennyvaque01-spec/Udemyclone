using UdemyClone.Application.Models.DTOs;
using UdemyClone.Application.Models.Requests.Estudiante;

namespace UdemyClone.Application.Interfaces
{
    public interface IEstudianteService
    {
        Task<List<EstudianteDto>> GetAll();
        Task<EstudianteDto?> GetById(int id);
        Task<EstudianteDto> Create(CreateEstudianteRequest model);
        Task<EstudianteDto?> Update(int id, UpdateEstudianteRequest model);
        Task<bool> Delete(int id);
    }
}
