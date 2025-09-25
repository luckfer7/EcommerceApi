using EcommerceApi.Models;

namespace EcommerceApi.Repositories
{
    public class CarrinhoRepository
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
