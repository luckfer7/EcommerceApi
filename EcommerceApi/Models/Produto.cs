namespace EcommerceApi.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }

        // Relacionamento com Categoria
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        public bool EmEstoque { get; set; } = true;
    }
}
