namespace EcommerceApi.Models
{
    public class Favorito
    {

        //aqui é a entidade favorito
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
    }
}
