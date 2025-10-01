using EcommerceApi.DTOs;
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

        public async Task AdicionarItemAoCarrinhoAsync(CarrinhoDto dto)
        {
            var carrinhoItem = new CarrinhoItem
            {
                UsuarioId = dto.UsuarioId,
                ProdutoId = dto.ProdutoId

            };
            await _repo.AdicionarItemAoCarrinhoAsync(carrinhoItem);
        }
    }
}
