namespace EcommerceApi.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Lista de produtos associados
        public ICollection<Produto> Produtos { get; set; }
    }
}
