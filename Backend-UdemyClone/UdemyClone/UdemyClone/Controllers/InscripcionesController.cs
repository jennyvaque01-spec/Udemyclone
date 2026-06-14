using Microsoft.AspNetCore.Mvc;
using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models.Requests.Estudiante;

namespace UdemyClone.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscripcionesController(IInscripcionService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resultado = await service.GetAll();
            return Ok(resultado);
        }

        [HttpGet("estudiante/{estudianteId:int}")]
        public async Task<IActionResult> GetByEstudiante(int estudianteId)
            => Ok(await service.GetByEstudiante(estudianteId));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var resultado = await service.GetById(id);
            return resultado is null ? NotFound("Inscripción no encontrada") : Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInscripcionRequest model)
        {
            var resultado = await service.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = resultado.InscripcionId }, resultado);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await service.Delete(id);
            return eliminado ? NoContent() : NotFound("Inscripción no encontrada");
        }
    }
}
