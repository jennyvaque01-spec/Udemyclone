using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models.Requests.Estudiante;

namespace UdemyClone.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController(IEstudianteService service) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resultado = await service.GetAll();
            return Ok(resultado);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var resultado = await service.GetById(id);
            if (resultado is null) return NotFound("Estudiante no encontrado");
            return Ok(resultado);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEstudianteRequest model)
        {
            var resultado = await service.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = resultado.EstudianteId }, resultado);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEstudianteRequest model)
        {
            var resultado = await service.Update(id, model);
            if (resultado is null) return NotFound("Estudiante no encontrado");
            return Ok(resultado);
        }


        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await service.Delete(id);
            if (!eliminado) return NotFound("Estudiante no encontrado");
            return NoContent();
        }
    }
}
