namespace EcommerceApi.DTOs
{

    //Esse dto será a entrada (criação ou edição)
    public class ProdutoCreateDto
    {
        public string Slug { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }
        public bool EmEstoque { get; set; } = true;
        public int CategoriaId { get; set; }
    }
}
