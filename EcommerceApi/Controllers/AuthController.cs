using EcommerceApi.DTOs;
using EcommerceApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
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
                    message = "Usuário egistrado com sucesso",
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
                var token = _authService.Login(dto);
                return Ok(new
                {
                    token
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }
    }
}
