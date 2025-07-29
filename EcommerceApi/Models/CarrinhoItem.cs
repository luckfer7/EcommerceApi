namespace EcommerceApi.Models
{
    public class CarrinhoItem
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
    }
}
