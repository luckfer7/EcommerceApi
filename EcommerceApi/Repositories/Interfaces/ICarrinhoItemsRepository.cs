using EcommerceApi.Models;

namespace EcommerceApi.Repositories.Interfaces
{
    public interface ICarrinhoItemsRepository
    {
        Task AdicionarItemAoCarrinhoAsync(CarrinhoItem carrinhoItem);
    }
}
