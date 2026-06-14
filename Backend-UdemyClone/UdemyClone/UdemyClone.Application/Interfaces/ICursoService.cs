using UdemyClone.Application.Models.DTOs;
using UdemyClone.Application.Models.Requests.Estudiante;

namespace UdemyClone.Application.Interfaces
{
    public interface ICursoService
    {
        Task<List<CursoDto>> GetAll();
        Task<CursoDto?> GetById(int id);
        Task<CursoDto> Create(CreateCursoRequest model);
        Task<CursoDto?> Update(int id, CreateCursoRequest model);
        Task<bool> Delete(int id);
    }
}
