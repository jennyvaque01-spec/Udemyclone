using Microsoft.AspNetCore.Mvc;
using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models.Requests.Estudiante;

namespace UdemyClone.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgresoController(IProgresoService service) : ControllerBase
    {
        [HttpGet("estudiante/{estudianteId:int}")]
        public async Task<IActionResult> GetByEstudiante(int estudianteId)
            => Ok(await service.GetByEstudiante(estudianteId));

        [HttpGet("avance/{estudianteId:int}/curso/{cursoId:int}")]
        public async Task<IActionResult> GetPorcentaje(int estudianteId, int cursoId)
        {
            var porcentaje = await service.GetPorcentajeAvance(estudianteId, cursoId);
            return Ok(new { EstudianteId = estudianteId, CursoId = cursoId, PorcentajeAvance = porcentaje });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProgresoRequest model)
            => Ok(await service.Create(model));

        [HttpDelete("{estudianteId:int}/{leccionId:int}")]
        public async Task<IActionResult> Delete(int estudianteId, int leccionId)
        {
            var eliminado = await service.Delete(estudianteId, leccionId);
            return eliminado ? NoContent() : NotFound("Progreso no encontrado");
        }
    }

}
