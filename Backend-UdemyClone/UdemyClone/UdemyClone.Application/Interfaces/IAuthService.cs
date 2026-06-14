using UdemyClone.Application.Models.DTOs;
using UdemyClone.Application.Models.Requests.Estudiante;

namespace UdemyClone.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Login(LoginRequest model);
        Task<AuthResponseDto> Register(RegisterRequest model);
    }
}
