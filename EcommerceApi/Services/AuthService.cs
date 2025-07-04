using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EcommerceApi.DTOs;
using EcommerceApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace EcommerceApi.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context; //Acesso ao banco de dados

        private readonly IConfiguration _config; //acesso as configurações do app (como a chave JWT)

        //Construtor com injeção de dependência do context e config
        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // Método para registrar um novo usuário
        public Usuario Registrar(RegisterDto dto)
        {
            var senhaCriptografada = BCrypt.Net.BCrypt.HashPassword(dto.Senha);

            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = senhaCriptografada
            };

            // Adiciona o usuário ao banco e salva as mudanças
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            // Retorna o usuário recém-criado
            return usuario;
        }

        // Método para autenticar um usuário e gerar um token JWT
        public string Login(LoginDto dto)
        {
            // Procura o usuário no banco pelo email
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == dto.Email);

            // Se não encontrar ou se a senha não bater, lança exceção
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))
            {
                throw new Exception("Credenciais inválidas");
            }

            // Se autenticado, gera e retorna o token JWT
            return GerarToken(usuario);
        }

        // Método privado para gerar o token JWT do usuário
        private string GerarToken(Usuario usuario)
        {
            // Define os dados (claims) que vão dentro do token
            var claims = new[]
            {
                // ID do usuário
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),

                //Email do usuario
                new Claim(ClaimTypes.Email, usuario.Email), 

                //Nome do usuario
                new Claim(ClaimTypes.Name, usuario.Nome)
            };

            // Cria a chave de segurança baseada na string definida no appsettings.json
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            // Define o algoritmo de assinatura (HMAC SHA256)
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            //// Cria o token com as claims, tempo de expiração e credenciais de assinatura
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credenciais
            );

            // Converte o token em string e retorna
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
