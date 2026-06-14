using Microsoft.AspNetCore.Mvc;
using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models.Requests.Estudiante;

namespace UdemyClone.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResenasController(IResenaService service) : ControllerBase
    {
        [HttpGet("curso/{cursoId:int}")]
        public async Task<IActionResult> GetByCurso(int cursoId)
            => Ok(await service.GetByCurso(cursoId));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateResenaRequest model)
        {
            var resultado = await service.Create(model);
            return Ok(resultado);
        }

        [HttpDelete("{estudianteId:int}/{cursoId:int}")]
        public async Task<IActionResult> Delete(int estudianteId, int cursoId)
        {
            var eliminado = await service.Delete(estudianteId, cursoId);
            return eliminado ? NoContent() : NotFound("Reseña no encontrada");
        }
    }
}
