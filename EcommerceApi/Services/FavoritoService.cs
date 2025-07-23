using EcommerceApi.Models;
using EcommerceApi.Repositories.Interfaces;

namespace EcommerceApi.Services
{
    public class FavoritoService
    {
        private readonly IFavoritoRepository _repo;

        public FavoritoService(IFavoritoRepository repo)
        {
            _repo = repo;
        }

        public async Task AdicionarAsync(int usuarioId, int produtoId)
        {
            if (await _repo.ExisteAsync(usuarioId, produtoId))
                throw new Exception("Produto já favoritado");

            var favorito = new Favorito
            {
                UsuarioId = usuarioId,
                ProdutoId = produtoId
            };

            await _repo.AdicionarFavoritoAsync(favorito);
        }

        public async Task RemoverAsync(int usuarioId, int produtoId)
        {
            await _repo.RemoverFavoritoAsync(usuarioId, produtoId);
        }

        public async Task<List<Produto>> ListarAsync(int usuarioId)
        {
            return await _repo.ListarFavoritosAsync(usuarioId);
        }
    }
}
