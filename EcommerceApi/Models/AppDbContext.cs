using EcommerceApi.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options
            ) : base(options) { }

        public DbSet <Usuario> Usuarios { get; set; }
        public DbSet <Produto> Produtos { get; set; }
        public DbSet <Categoria> Categorias { get; set; }
        public DbSet <Favorito> Favoritos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new FavoritoConfiguration());
            // Aqui você vai usar modelBuilder.ApplyConfiguration() no futuro
        }
    }
}
