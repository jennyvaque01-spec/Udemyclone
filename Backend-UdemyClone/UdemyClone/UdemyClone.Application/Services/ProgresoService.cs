using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models.DTOs;
using UdemyClone.Application.Models.Requests.Estudiante;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Application.Services
{
    public class ProgresoService(IProgresoRepository repo) : IProgresoService
    {
        public async Task<List<ProgresoDto>> GetByEstudiante(int estudianteId)
        {
            var lista = await repo.GetByEstudiante(estudianteId);
            return lista.Select(MapToDto).ToList();
        }

        public async Task<ProgresoDto> Create(CreateProgresoRequest model)
        {
            if (await repo.Existe(model.EstudianteId, model.LeccionId))
                throw new Exception("Esta lección ya fue marcada como completada");

            var progreso = new Progreso
            {
                EstudianteId = model.EstudianteId,
                LeccionId = model.LeccionId
            };
            var creado = await repo.Create(progreso);
            return MapToDto(creado);
        }

        public async Task<bool> Delete(int estudianteId, int leccionId)
            => await repo.Delete(estudianteId, leccionId);

        public async Task<double> GetPorcentajeAvance(int estudianteId, int cursoId)
            => await repo.GetPorcentajeAvance(estudianteId, cursoId);

        private static ProgresoDto MapToDto(Progreso p) => new()
        {
            EstudianteId = p.EstudianteId,
            LeccionId = p.LeccionId,
            LeccionTitulo = p.Leccion?.Titulo ?? "",
            FechaTerminada = p.FechaTerminada
        };
    }
}
