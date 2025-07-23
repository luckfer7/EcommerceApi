using System.Security.Claims;
using EcommerceApi.DTOs;
using EcommerceApi.Models;
using EcommerceApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace EcommerceApi.Controllers
{
    [ApiController]
    [Route("favoritos")]
    [Authorize]
    public class FavoritosController : ControllerBase
    {
        private readonly FavoritoService _service;

        public FavoritosController(FavoritoService service) 
        {
            _service = service;
        }
        
        private int ObterUsuarioId() => 
            int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] FavoritoDto dto)
        {
            await _service.AdicionarAsync(ObterUsuarioId(), dto.ProdutoId);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var favoritos = await _service.ListarAsync(ObterUsuarioId());
            return Ok(favoritos);
        }

        [HttpDelete("{produtoId}")]
        public async Task<IActionResult> Remover(int produtoId)
        {
            await _service.RemoverAsync(ObterUsuarioId(), produtoId);
            return NoContent();
        }
    }
}

//Depois disso, podemos testar pelo Swagger:

//POST / favoritos com { "produtoId": 1 }

//GET / favoritos

//DELETE / favoritos / 1
