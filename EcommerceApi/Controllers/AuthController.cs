using System.Security.Claims;
using EcommerceApi.DTOs;
using EcommerceApi.Models;
using EcommerceApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        //POST /auth/register
        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            try
            {
                var usuario = _authService.Registrar(dto);
                return Ok(new
                {
                    message = "Usuário registrado com sucesso",
                    usuario = new { usuario.Id, usuario.Nome, usuario.Email }

                });
            }
            catch(Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        //POST /auth/login
        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            try
            {
                var response = _authService.Login(dto);

                return Ok(response);                
            }
            catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetUser()
        //Essa é a rota protegida que retorna os dados do usuário logado.
        {

            //esse user.findfirst Recupera os dados (claims) salvos no token
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var nome = User.FindFirst(ClaimTypes.Name)?.Value;

            return Ok(new
            {
                //Retorna os dados do usuário no formato JSON
                id = userId,
                email,
                nome
            });
        }
    }
}
