using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models.DTOs;
using UdemyClone.Application.Models.Requests.Estudiante;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Application.Services
{
    public class InstructorService(IInstructorRepository repo) : IInstructorService
    {
        public async Task<List<InstructorDto>> GetAll()
        {
            var lista = await repo.GetAll();
            return lista.Select(MapToDto).ToList();
        }

        public async Task<InstructorDto?> GetById(int id)
        {
            var instructor = await repo.GetById(id);
            return instructor is null ? null : MapToDto(instructor);
        }

        public async Task<InstructorDto> Create(CreateInstructorRequest model)
        {
            if (await repo.ExisteEmail(model.Email))
                throw new Exception("Ya existe un instructor con ese email");

            var instructor = new Instructor
            {
                Nombre = model.Nombre,
                Email = model.Email
            };

            var creado = await repo.Create(instructor);
            return MapToDto(creado);
        }

        public async Task<InstructorDto?> Update(int id, CreateInstructorRequest model)
        {
            var datos = new Instructor { Nombre = model.Nombre, Email = model.Email };
            var actualizado = await repo.Update(id, datos);
            return actualizado is null ? null : MapToDto(actualizado);
        }

        public async Task<bool> Delete(int id) => await repo.Delete(id);

        private static InstructorDto MapToDto(Instructor i) => new()
        {
            InstructorId = i.InstructorId,
            Nombre = i.Nombre,
            Email = i.Email,
            FechaRegistro = i.FechaRegistro
        };
    }
}
