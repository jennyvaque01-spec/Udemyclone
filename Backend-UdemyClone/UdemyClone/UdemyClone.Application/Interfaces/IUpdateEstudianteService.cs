using UdemyClone.Application.Models.Requests.Estudiante;

namespace UdemyClone.Application.Interfaces
{
    public interface IUpdateEstudianteService
    {
        Task<bool> CreateEstudianteAsync(CreateEstudianteRequest request);
    }
}
