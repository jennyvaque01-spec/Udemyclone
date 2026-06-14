using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models.DTOs;
using UdemyClone.Application.Models.Requests.Estudiante;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Application.Services
{
    public class ResenaService(IResenaRepository repo, IInscripcionRepository inscRepo) : IResenaService
    {
        public async Task<List<ResenaDto>> GetByCurso(int cursoId)
        {
            var lista = await repo.GetByCurso(cursoId);
            return lista.Select(MapToDto).ToList();
        }

        public async Task<ResenaDto> Create(CreateResenaRequest model)
        {

            if (!await inscRepo.YaInscrito(model.EstudianteId, model.CursoId))
                throw new Exception("El estudiante debe estar inscrito para dejar una reseña");


            if (await repo.YaReseno(model.EstudianteId, model.CursoId))
                throw new Exception("El estudiante ya dejó una reseña en este curso");

            var resena = new Resena
            {
                EstudianteId = model.EstudianteId,
                CursoId = model.CursoId,
                Calificacion = model.Calificacion,
                ResenaTexto = model.ResenaTexto
            };
            var creada = await repo.Create(resena);
            return MapToDto(creada);
        }

        public async Task<bool> Delete(int estudianteId, int cursoId)
            => await repo.Delete(estudianteId, cursoId);

        private static ResenaDto MapToDto(Resena r) => new()
        {
            EstudianteId = r.EstudianteId,
            EstudianteNombre = r.Estudiante?.Nombre ?? "",
            CursoId = r.CursoId,
            CursoTitulo = r.Curso?.Titulo ?? "",
            Calificacion = r.Calificacion,
            ResenaTexto = r.ResenaTexto,
            Fecha = r.Fecha
        };
    }
}
