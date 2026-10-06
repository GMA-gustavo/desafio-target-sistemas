using Desafio2.Estoque.Models;

namespace Desafio2.Estoque.Services;

public class ControleEstoque
{
    private readonly Dictionary<int, Produto> _produtos;
    private readonly List<Movimentacao> _movimentacoes = new();
    private int _proximoId = 1;

    public ControleEstoque(IEnumerable<Produto> produtos)
    {
        _produtos = produtos.ToDictionary(p => p.CodigoProduto);
    }

    public IReadOnlyCollection<Produto> Produtos => _produtos.Values;
    public IReadOnlyList<Movimentacao> Movimentacoes => _movimentacoes;

    // Registra uma entrada ou saída, atualiza o saldo do produto e devolve a movimentação gerada
    public Movimentacao Movimentar(int codigoProduto, TipoMovimentacao tipo, int quantidade, string descricao)
    {
        if (!_produtos.TryGetValue(codigoProduto, out var produto))
            throw new ArgumentException($"Produto {codigoProduto} não encontrado.");

        if (quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.");

        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Informe uma descrição para a movimentação.");

        if (tipo == TipoMovimentacao.Saida && quantidade > produto.Estoque)
            throw new InvalidOperationException(
                $"Estoque insuficiente. Saldo atual de {produto.DescricaoProduto}: {produto.Estoque}.");

        produto.Estoque += tipo == TipoMovimentacao.Entrada ? quantidade : -quantidade;

        var movimentacao = new Movimentacao(
            _proximoId++,
            produto.CodigoProduto,
            tipo,
            quantidade,
            descricao.Trim(),
            DateTime.Now,
            produto.Estoque);

        _movimentacoes.Add(movimentacao);
        return movimentacao;
    }
}
