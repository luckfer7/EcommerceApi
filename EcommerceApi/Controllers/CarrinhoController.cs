using EcommerceApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [ApiController]
    [Route("carrinho")]
    public class CarrinhoController : ControllerBase
    {

        private readonly CarrinhoItemService _service;

        public CarrinhoController(CarrinhoItemService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarItemAoCarrinho(int Id, int produtoId, Models.Produto produto)
        {
            await _service.AdicionarItemAoCarrinhoAsync(Id, produtoId, produto);
            return Ok();
        }
    }
}
