namespace Desafio1.Comissoes.Models;

public record Venda(string Vendedor, decimal Valor);

// Estrutura raiz do JSON: { "vendas": [ ... ] }
public record ArquivoVendas(List<Venda> Vendas);
