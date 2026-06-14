using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models.DTOs;
using UdemyClone.Application.Models.Requests.Estudiante;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Application.Services
{
    public class CursoService(ICursoRepository repo) : ICursoService
    {
        public async Task<List<CursoDto>> GetAll()
        {
            var lista = await repo.GetAll();
            return lista.Select(MapToDto).ToList();
        }

        public async Task<CursoDto?> GetById(int id)
        {
            var curso = await repo.GetById(id);
            return curso is null ? null : MapToDto(curso);
        }

        public async Task<CursoDto> Create(CreateCursoRequest model)
        {
            var curso = new Curso
            {
                Titulo = model.Titulo,
                Descripcion = model.Descripcion,
                Precio = model.Precio,
                Idioma = model.Idioma,
                Nivel = model.Nivel,
                CategoriaId = model.CategoriaId
            };
            var creado = await repo.Create(curso);
            return MapToDto(creado);
        }

        public async Task<CursoDto?> Update(int id, CreateCursoRequest model)
        {
            var datos = new Curso
            {
                Titulo = model.Titulo,
                Descripcion = model.Descripcion,
                Precio = model.Precio,
                Idioma = model.Idioma,
                Nivel = model.Nivel,
                CategoriaId = model.CategoriaId
            };
            var actualizado = await repo.Update(id, datos);
            return actualizado is null ? null : MapToDto(actualizado);
        }

        public async Task<bool> Delete(int id) => await repo.Delete(id);

        private static CursoDto MapToDto(Curso c) => new()
        {
            CursoId = c.CursoId,
            Titulo = c.Titulo,
            Descripcion = c.Descripcion,
            Precio = c.Precio,
            Idioma = c.Idioma,
            Nivel = c.Nivel,
            CategoriaId = c.CategoriaId,
            CategoriaNombre = c.Categoria?.Nombre ?? ""
        };
    }
}
