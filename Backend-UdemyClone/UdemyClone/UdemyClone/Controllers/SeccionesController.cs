using Microsoft.AspNetCore.Mvc;
using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models.Requests.Estudiante;

namespace UdemyClone.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeccionesController(ISeccionService service) : ControllerBase
    {
        [HttpGet("curso/{cursoId:int}")]
        public async Task<IActionResult> GetByCurso(int cursoId)
            => Ok(await service.GetByCurso(cursoId));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var resultado = await service.GetById(id);
            return resultado is null ? NotFound("Sección no encontrada") : Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSeccionRequest model)
        {
            var resultado = await service.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = resultado.SeccionId }, resultado);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateSeccionRequest model)
        {
            var resultado = await service.Update(id, model);
            return resultado is null ? NotFound("Sección no encontrada") : Ok(resultado);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await service.Delete(id);
            return eliminado ? NoContent() : NotFound("Sección no encontrada");
        }
    }
}
