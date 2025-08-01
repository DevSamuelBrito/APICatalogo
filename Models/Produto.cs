
namespace APICatalogo.Models
{
    public class Produto
    {
        public int ProdutoID { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public string? ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CategoriaID { get; set; }
        public Categoria? Categoria { get; set; }
    }
}