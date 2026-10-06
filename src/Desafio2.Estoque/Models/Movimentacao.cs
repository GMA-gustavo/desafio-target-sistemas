namespace Desafio2.Estoque.Models;

public enum TipoMovimentacao
{
    Entrada,
    Saida
}

public record Movimentacao(
    int Id,
    int CodigoProduto,
    TipoMovimentacao Tipo,
    int Quantidade,
    string Descricao,
    DateTime Data,
    int EstoqueFinal);

public static class TipoMovimentacaoExtensions
{
    // Texto amigável (com acento) para exibir no console
    public static string ParaTexto(this TipoMovimentacao tipo) =>
        tipo == TipoMovimentacao.Entrada ? "Entrada" : "Saída";
}
