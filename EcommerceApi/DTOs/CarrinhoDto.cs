namespace EcommerceApi.DTOs
{
    public class CarrinhoDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int ProdutoId { get; set; }
        public ProdutoDto Produto { get; set; }
    }
}
