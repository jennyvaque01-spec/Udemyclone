using Microsoft.AspNetCore.Mvc;
using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models.Requests.Estudiante;

namespace UdemyClone.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            var resultado = await service.Register(model);
            return Ok(resultado);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var resultado = await service.Login(model);
            return Ok(resultado);
        }
    }
}
