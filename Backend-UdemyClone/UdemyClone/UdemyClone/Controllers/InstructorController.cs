using Microsoft.AspNetCore.Mvc;
using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models.Requests.Estudiante;

namespace UdemyClone.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructoresController(IInstructorService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await service.GetAll());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var resultado = await service.GetById(id);
            return resultado is null ? NotFound("Instructor no encontrado") : Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInstructorRequest model)
        {
            var resultado = await service.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = resultado.InstructorId }, resultado);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateInstructorRequest model)
        {
            var resultado = await service.Update(id, model);
            return resultado is null ? NotFound("Instructor no encontrado") : Ok(resultado);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await service.Delete(id);
            return eliminado ? NoContent() : NotFound("Instructor no encontrado");
        }
    }
}
