using EcommerceApi.DTOs;
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
        public async Task<IActionResult> AdicionarItemAoCarrinho([FromBody] CarrinhoDto carrinhoDto)
        {
            await _service.AdicionarItemAoCarrinhoAsync(carrinhoDto);
            return Ok(new { message = "Item adicionado ao carrinho com sucesso" });
        }
    }
}
