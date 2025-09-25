using EcommerceApi.Models;
using EcommerceApi.Repositories.Interfaces;

namespace EcommerceApi.Services
{
    public class CarrinhoItemService
    {
        private readonly ICarrinhoItemsRepository _repo;

        public CarrinhoItemService(ICarrinhoItemsRepository repo)
        {
            _repo = repo;
        }

        public async Task AdicionarItemAoCarrinhoAsync(int Id, int produtoId, Produto produto)
        {
            var carrinhoItem = new Models.CarrinhoItem
            {
                Id = Id,
                ProdutoId = produtoId,
                Produto = produto

            };
            await _repo.AdicionarItemAoCarrinhoAsync(carrinhoItem);
        }
    }
}
