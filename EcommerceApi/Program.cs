using System.Text;
using EcommerceApi.Models;
using EcommerceApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce API", Version = "v1" });

    // Adiciona o esquema de segurança JWT
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira: Bearer {seu token}"
    });

    // Requer o token nas rotas com [Authorize]
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<CategoriaService>();

//Pega a chave secreta do appsettings.json (Jwt:Key)
//Converte essa chave em um array de bytes, pois o JWT precisa disso para criptografar e validar o tokenPega a chave secreta do appsettings.json (Jwt:Key)
//Converte essa chave em um array de bytes, pois o JWT precisa disso para criptografar e validar o token
var key = builder.Configuration["Jwt:Key"];
var keyBytes = Encoding.UTF8.GetBytes(key);

//Esse bloco ativa a autenticação por token JWT.
builder.Services.AddAuthentication(options =>
{
    //DefaultAuthenticateScheme define o esquema padrão para autenticar (JWT)
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;


    //DefaultChallengeScheme define o que usar quando o usuário não estiver autenticado
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

    //Configura o comportamento do JWT
    .AddJwtBearer(options =>
    {

        
        options.RequireHttpsMetadata = false; //Permite rodar sem HTTPS em dev

        options.SaveToken = true; //Salva o token no contexto da requisição

        options.TokenValidationParameters = new TokenValidationParameters
            //Define como o token será validado
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes), // nossa chave secreta
            ValidateIssuer = false, // desliga validação de emissor
            ValidateAudience = false // desliga validação de público
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
