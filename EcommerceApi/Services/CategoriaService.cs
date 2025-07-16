using EcommerceApi.DTOs;
using EcommerceApi.Models;

namespace EcommerceApi.Services
{
    public class CategoriaService
    {
        private readonly AppDbContext _context;

        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }

        public List<CategoriaDto> Listar()
        {
            return _context.Categorias
                .Select(c => new CategoriaDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
        }

        public void Criar(CategoriaDto dto)
        {
            var categoria = new Categoria
            {
                Name = dto.Name
            };

            _context.Categorias.Add(categoria);
            _context.SaveChanges();


        }

    }
}
