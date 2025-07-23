using EcommerceApi.Models;

namespace EcommerceApi.Repositories.Interfaces
{
    public interface IFavoritoRepository
    {
        Task AdicionarFavoritoAsync(Favorito favorito);
        Task RemoverFavoritoAsync(int usuarioId, int produtoId);
        Task<List<Produto>> ListarFavoritosAsync(int usuarioId);
        Task<bool> ExisteAsync(int usuarioId, int produtoId);

    }
}
