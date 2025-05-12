namespace Produto.Domain
{
    public class ProdutoCore
    {
        public int ID { get; set; }
        public string NOME { get; set; } = string.Empty;
        public string DESCRICAO { get; set; } = string.Empty;
        public decimal VALOR { get; set; }
    }
}
