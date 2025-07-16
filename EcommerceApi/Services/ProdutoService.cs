using EcommerceApi.DTOs;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EcommerceApi.Services
{
    //Esses serviços vão conter a lógica de negócio para lidar com dados, como buscar no banco, criar, editar, deletar, etc.

    public class ProdutoService
    {
        private readonly AppDbContext _context;

        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }

        public List<ProdutoDto> ListarTodos()
        {
            return _context.Produtos
                .Include(p => p.Categoria)
                .Select(p => new ProdutoDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Slug = p.Slug,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    EmEstoque = p.EmEstoque,
                    Categoria = new CategoriaDto
                    {
                        Id = p.Categoria.Id,
                        Name = p.Categoria.Name
                    }
                })
                .ToList();
        }

        public ProdutoDto BuscarPorId(int id)
        {
            var p = _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefault(p => p.Id == id);

            if (p == null) throw new Exception("Produto não encontrado");


            return new ProdutoDto
            {
                Id = p.Id,
                Name = p.Name,
                Slug = p.Slug,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                EmEstoque = p.EmEstoque,
                Categoria = new CategoriaDto
                {
                    Id = p.Categoria.Id,
                    Name = p.Categoria.Name
                }
            };
        }

        public void Criar(ProdutoCreateDto dto)
        {
            var produto = new Produto
            {
                Name = dto.Name,
                Slug = dto.Slug,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                EmEstoque = dto.EmEstoque,
                CategoriaId = dto.CategoriaId
            };

            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }

        public void Atualizar(int id, ProdutoCreateDto dto)
        {
            var produto = _context.Produtos.Find(id);

            if (produto == null) throw new Exception("Produto não encontrado");

            produto.Name = dto.Name;
            produto.Slug = dto.Slug;
            produto.Description = dto.Description;
            produto.Price = dto.Price;
            produto.ImageUrl = dto.ImageUrl;
            produto.EmEstoque = dto.EmEstoque;
            produto.CategoriaId = dto.CategoriaId;

            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto == null) throw new Exception("Produto não encontrado");

            _context.Produtos.Remove(produto);
            _context.SaveChanges();
        }

        public List<ProdutoDto> BuscarPorCategoria(int categoriaId)
        {
            return _context.Produtos
                .Include(p => p.Categoria)
                .Where(p => p.CategoriaId == categoriaId)
                .Select(p => new ProdutoDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Slug = p.Slug,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    EmEstoque = p.EmEstoque,
                    Categoria = new CategoriaDto
                    {
                        Id = p.Categoria.Id,
                        Name = p.Categoria.Name
                    }
                })
                .ToList();
        }
    }
}
