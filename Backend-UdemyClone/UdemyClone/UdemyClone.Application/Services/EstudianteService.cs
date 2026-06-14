using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models.DTOs;
using UdemyClone.Application.Models.Requests.Estudiante;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Application.Services
{
    public class EstudianteService(IEstudianteRepository repo) : IEstudianteService
    {
        public async Task<List<EstudianteDto>> GetAll()
        {
            var estudiantes = await repo.GetAll();

            return estudiantes.Select(MapToDto).ToList();
        }

        public async Task<EstudianteDto?> GetById(int id)
        {
            var estudiante = await repo.GetById(id);

            return estudiante is null ? null : MapToDto(estudiante);
        }

        public async Task<EstudianteDto> Create(CreateEstudianteRequest model)
        {

            if (await repo.ExisteEmail(model.Email))
                throw new Exception("Ya existe un estudiante con ese email");

            var estudiante = new Estudiante
            {
                Nombre = model.Nombre,
                Email = model.Email
            };

            var creado = await repo.Create(estudiante);
            return MapToDto(creado);
        }

        public async Task<EstudianteDto?> Update(int id, UpdateEstudianteRequest model)
        {
            var actualizado = await repo.Update(id, new Estudiante { Nombre = model.Nombre });
            return actualizado is null ? null : MapToDto(actualizado);
        }

        public async Task<bool> Delete(int id)
        {
            return await repo.Delete(id);
        }

        private static EstudianteDto MapToDto(Estudiante e) => new()
        {
            EstudianteId = e.EstudianteId,
            Nombre = e.Nombre,
            Email = e.Email,
            FechaRegistro = e.FechaRegistro
        };
    }
}
