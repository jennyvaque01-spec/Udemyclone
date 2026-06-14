using UdemyClone.Application.Models.DTOs;
using UdemyClone.Application.Models.Requests.Estudiante;

namespace UdemyClone.Application.Interfaces
{
    public interface IResenaService
    {
        Task<List<ResenaDto>> GetByCurso(int cursoId);
        Task<ResenaDto> Create(CreateResenaRequest model);
        Task<bool> Delete(int estudianteId, int cursoId);
    }
}
