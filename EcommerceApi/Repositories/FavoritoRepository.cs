using EcommerceApi.Models;
using EcommerceApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repositories
{
    public class FavoritoRepository : IFavoritoRepository
    {
        private readonly AppDbContext _context;

        public FavoritoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarFavoritoAsync(Favorito favorito)
        {
            _context.Favoritos.Add(favorito);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverFavoritoAsync(int usuarioId, int produtoId)
        {
            var favorito = await _context.Favoritos
                .FirstOrDefaultAsync(f => f.UsuarioId == usuarioId && f.ProdutoId == produtoId);

            if (favorito != null)
            {
                _context.Favoritos.Remove(favorito);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Produto>> ListarFavoritosAsync(int usuarioId)
        {
            return await _context.Favoritos
                .Where(f => f.UsuarioId == usuarioId)
                .Select(f => f.Produto)
                .ToListAsync();
        }

        public async Task<bool> ExisteAsync(int usuarioId, int produtoId)
        {
            return await _context.Favoritos.AnyAsync(f => f.UsuarioId == usuarioId && f.ProdutoId == produtoId);
        }
    }
}
