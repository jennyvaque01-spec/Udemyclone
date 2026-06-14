using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models.DTOs;
using UdemyClone.Application.Models.Requests.Estudiante;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Application.Services
{
    public class SeccionService(ISeccionRepository repo) : ISeccionService
    {
        public async Task<List<SeccionDto>> GetByCurso(int cursoId)
        {
            var lista = await repo.GetByCurso(cursoId);
            return lista.Select(MapToDto).ToList();
        }

        public async Task<SeccionDto?> GetById(int id)
        {
            var seccion = await repo.GetById(id);
            return seccion is null ? null : MapToDto(seccion);
        }

        public async Task<SeccionDto> Create(CreateSeccionRequest model)
        {
            var seccion = new Seccion
            {
                CursoId = model.CursoId,
                Titulo = model.Titulo,
                Orden = model.Orden
            };
            var creada = await repo.Create(seccion);
            return MapToDto(creada);
        }

        public async Task<SeccionDto?> Update(int id, CreateSeccionRequest model)
        {
            var datos = new Seccion
            {
                CursoId = model.CursoId,
                Titulo = model.Titulo,
                Orden = model.Orden
            };
            var actualizada = await repo.Update(id, datos);
            return actualizada is null ? null : MapToDto(actualizada);
        }

        public async Task<bool> Delete(int id) => await repo.Delete(id);

        private static SeccionDto MapToDto(Seccion s) => new()
        {
            SeccionId = s.SeccionId,
            CursoId = s.CursoId,
            Titulo = s.Titulo,
            Orden = s.Orden
        };
    }
}
