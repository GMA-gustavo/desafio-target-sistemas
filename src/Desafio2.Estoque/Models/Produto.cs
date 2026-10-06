namespace Desafio2.Estoque.Models;

public class Produto
{
    public int CodigoProduto { get; set; }
    public string DescricaoProduto { get; set; } = string.Empty;
    public int Estoque { get; set; }
}

// Estrutura raiz do JSON: { "estoque": [ ... ] }
public record ArquivoEstoque(List<Produto> Estoque);
