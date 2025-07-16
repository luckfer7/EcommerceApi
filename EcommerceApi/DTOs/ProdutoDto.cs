namespace EcommerceApi.DTOs
{

    //Esse dto será usado como saída (retorno da API)
    public class ProdutoDto
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }
        public bool EmEstoque { get; set; }

        public CategoriaDto Categoria { get; set; }
    }
}
