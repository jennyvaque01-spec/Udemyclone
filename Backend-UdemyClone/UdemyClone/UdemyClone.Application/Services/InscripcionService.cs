using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models.DTOs;
using UdemyClone.Application.Models.Requests.Estudiante;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Application.Services
{
    public class InscripcionService(IInscripcionRepository repo) : IInscripcionService
    {


        public async Task<List<InscripcionDto>> GetAll()
        {
            var lista = await repo.GetAll();
            return lista.Select(MapToDto).ToList();
        }

        public async Task<List<InscripcionDto>> GetByEstudiante(int estudianteId)
        {
            var lista = await repo.GetByEstudiante(estudianteId);
            return lista.Select(MapToDto).ToList();
        }

        public async Task<InscripcionDto?> GetById(int id)
        {
            var insc = await repo.GetById(id);
            return insc is null ? null : MapToDto(insc);
        }

        public async Task<InscripcionDto> Create(CreateInscripcionRequest model)
        {
            if (await repo.YaInscrito(model.EstudianteId, model.CursoId))
                throw new Exception("El estudiante ya está inscrito en este curso");

            var inscripcion = new Inscripcion
            {
                EstudianteId = model.EstudianteId,
                CursoId = model.CursoId,
                PrecioPagado = model.PrecioPagado,
                CuponCodigo = model.CuponCodigo
            };
            var creada = await repo.Create(inscripcion);
            return MapToDto(creada);
        }

        public async Task<bool> Delete(int id) => await repo.Delete(id);

        private static InscripcionDto MapToDto(Inscripcion i) => new()
        {
            InscripcionId = i.InscripcionId,
            EstudianteId = i.EstudianteId,
            EstudianteNombre = i.Estudiante?.Nombre ?? "",
            CursoId = i.CursoId,
            CursoTitulo = i.Curso?.Titulo ?? "",
            FechaInscripcion = i.FechaInscripcion,
            PrecioPagado = i.PrecioPagado,
            CuponCodigo = i.CuponCodigo
        };
    }
}
