using EcommerceApi.Models;
using EcommerceApi.Repositories.Interfaces;

namespace EcommerceApi.Repositories
{
    public class CarrinhoRepository : ICarrinhoItemsRepository
    {
        private readonly AppDbContext _context;

        public CarrinhoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarItemAoCarrinhoAsync(CarrinhoItem carrinhoItem)
        {
            _context.CarrinhoItem.Add(carrinhoItem);
            await _context.SaveChangesAsync();
        }
    }
}
