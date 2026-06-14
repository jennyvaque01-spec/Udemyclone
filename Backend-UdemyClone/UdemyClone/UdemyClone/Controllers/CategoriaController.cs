using Microsoft.AspNetCore.Mvc;
using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models.Requests.Estudiante;

namespace UdemyClone.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController(ICategoriaService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await service.GetAll());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var resultado = await service.GetById(id);
            return resultado is null ? NotFound("Categoría no encontrada") : Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoriaRequest model)
        {
            var resultado = await service.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = resultado.CategoriaId }, resultado);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateCategoriaRequest model)
        {
            var resultado = await service.Update(id, model);
            return resultado is null ? NotFound("Categoría no encontrada") : Ok(resultado);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await service.Delete(id);
            return eliminado ? NoContent() : NotFound("Categoría no encontrada");
        }
    }

}
