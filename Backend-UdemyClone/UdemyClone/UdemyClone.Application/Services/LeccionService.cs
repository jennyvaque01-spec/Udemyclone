using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models.DTOs;
using UdemyClone.Application.Models.Requests.Estudiante;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Application.Services
{
    public class LeccionService(ILeccionRepository repo) : ILeccionService
    {
        public async Task<List<LeccionDto>> GetBySeccion(int seccionId)
        {
            var lista = await repo.GetBySeccion(seccionId);
            return lista.Select(MapToDto).ToList();
        }

        public async Task<LeccionDto?> GetById(int id)
        {
            var leccion = await repo.GetById(id);
            return leccion is null ? null : MapToDto(leccion);
        }

        public async Task<LeccionDto> Create(CreateLeccionRequest model)
        {
            var leccion = new Leccion
            {
                SeccionId = model.SeccionId,
                TipoLeccionId = model.TipoLeccionId,
                Titulo = model.Titulo,
                DuracionSegundos = model.DuracionSegundos,
                Orden = model.Orden
            };
            var creada = await repo.Create(leccion);
            return MapToDto(creada);
        }

        public async Task<LeccionDto?> Update(int id, CreateLeccionRequest model)
        {
            var datos = new Leccion
            {
                SeccionId = model.SeccionId,
                TipoLeccionId = model.TipoLeccionId,
                Titulo = model.Titulo,
                DuracionSegundos = model.DuracionSegundos,
                Orden = model.Orden
            };
            var actualizada = await repo.Update(id, datos);
            return actualizada is null ? null : MapToDto(actualizada);
        }

        public async Task<bool> Delete(int id) => await repo.Delete(id);

        private static LeccionDto MapToDto(Leccion l) => new()
        {
            LeccionId = l.LeccionId,
            SeccionId = l.SeccionId,
            Titulo = l.Titulo,
            DuracionSegundos = l.DuracionSegundos,
            Orden = l.Orden,
            TipoLeccionNombre = l.TipoLeccion?.Nombre ?? ""
        };
    }
}
