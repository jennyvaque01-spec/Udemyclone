using UdemyClone.Application.Models.DTOs;
using UdemyClone.Application.Models.Requests.Estudiante;

namespace UdemyClone.Application.Interfaces
{
    public interface IInstructorService
    {
        Task<List<InstructorDto>> GetAll();
        Task<InstructorDto?> GetById(int id);
        Task<InstructorDto> Create(CreateInstructorRequest model);
        Task<InstructorDto?> Update(int id, CreateInstructorRequest model);
        Task<bool> Delete(int id);
    }
}
