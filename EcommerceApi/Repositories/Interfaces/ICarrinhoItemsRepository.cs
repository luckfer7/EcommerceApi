using EcommerceApi.Models;

namespace EcommerceApi.Repositories.Interfaces
{
    public class ICarrinhoItemsRepository
    {
        Task AdicionarItemAoCarrinhoAsync(CarrinhoItem carrinhoItem);
    }
}
