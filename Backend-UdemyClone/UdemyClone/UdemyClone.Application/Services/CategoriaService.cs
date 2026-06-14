using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models.DTOs;
using UdemyClone.Application.Models.Requests.Estudiante;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Application.Services
{
    public class CategoriaService(ICategoriaRepository repo) : ICategoriaService
    {
        public async Task<List<CategoriaDto>> GetAll()
        {
            var lista = await repo.GetAll();
            return lista.Select(MapToDto).ToList();
        }

        public async Task<CategoriaDto?> GetById(int id)
        {
            var cat = await repo.GetById(id);
            return cat is null ? null : MapToDto(cat);
        }

        public async Task<CategoriaDto> Create(CreateCategoriaRequest model)
        {
            var categoria = new Categoria
            {
                Nombre = model.Nombre,
                CategoriaPadreId = model.CategoriaPadreId
            };
            var creada = await repo.Create(categoria);
            return MapToDto(creada);
        }

        public async Task<CategoriaDto?> Update(int id, CreateCategoriaRequest model)
        {
            var datos = new Categoria
            {
                Nombre = model.Nombre,
                CategoriaPadreId = model.CategoriaPadreId
            };
            var actualizada = await repo.Update(id, datos);
            return actualizada is null ? null : MapToDto(actualizada);
        }

        public async Task<bool> Delete(int id) => await repo.Delete(id);

        private static CategoriaDto MapToDto(Categoria c) => new()
        {
            CategoriaId = c.CategoriaId,
            Nombre = c.Nombre,
            CategoriaPadreId = c.CategoriaPadreId,
            CategoriaPadreNombre = c.CategoriaPadre?.Nombre
        };
    }
}
