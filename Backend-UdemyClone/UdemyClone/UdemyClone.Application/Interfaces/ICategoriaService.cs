using UdemyClone.Application.Models.DTOs;
using UdemyClone.Application.Models.Requests.Estudiante;

namespace UdemyClone.Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDto>> GetAll();
        Task<CategoriaDto?> GetById(int id);
        Task<CategoriaDto> Create(CreateCategoriaRequest model);
        Task<CategoriaDto?> Update(int id, CreateCategoriaRequest model);
        Task<bool> Delete(int id);
    }
}
