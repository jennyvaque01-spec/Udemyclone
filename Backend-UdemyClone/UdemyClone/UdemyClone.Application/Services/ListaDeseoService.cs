using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models.DTOs;
using UdemyClone.Application.Models.Requests.Estudiante;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Application.Services
{
    public class ListaDeseoService(IListaDeseoRepository repo) : IListaDeseoService
    {
        public async Task<List<ListaDeseoDto>> GetByEstudiante(int estudianteId)
        {
            var lista = await repo.GetByEstudiante(estudianteId);
            return lista.Select(MapToDto).ToList();
        }

        public async Task<ListaDeseoDto> Create(CreateListaDeseoRequest model)
        {
            if (await repo.Existe(model.CursoId, model.EstudianteId))
                throw new Exception("El curso ya está en la lista de deseos");

            var item = new ListaDeseo
            {
                CursoId = model.CursoId,
                EstudianteId = model.EstudianteId
            };
            var creado = await repo.Create(item);
            return MapToDto(creado);
        }

        public async Task<bool> Delete(int cursoId, int estudianteId)
            => await repo.Delete(cursoId, estudianteId);

        private static ListaDeseoDto MapToDto(ListaDeseo l) => new()
        {
            CursoId = l.CursoId,
            CursoTitulo = l.Curso?.Titulo ?? "",
            CursoPrecio = l.Curso?.Precio ?? 0,
            EstudianteId = l.EstudianteId
        };
    }
}
