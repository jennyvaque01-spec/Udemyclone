using Microsoft.AspNetCore.Mvc;
using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models.Requests.Estudiante;

namespace UdemyClone.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListaDeseoController(IListaDeseoService service) : ControllerBase
    {
        [HttpGet("estudiante/{estudianteId:int}")]
        public async Task<IActionResult> GetByEstudiante(int estudianteId)
            => Ok(await service.GetByEstudiante(estudianteId));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateListaDeseoRequest model)
            => Ok(await service.Create(model));

        [HttpDelete("{cursoId:int}/{estudianteId:int}")]
        public async Task<IActionResult> Delete(int cursoId, int estudianteId)
        {
            var eliminado = await service.Delete(cursoId, estudianteId);
            return eliminado ? NoContent() : NotFound(" no encontrado en lista de deseos");
        }
    }

}
