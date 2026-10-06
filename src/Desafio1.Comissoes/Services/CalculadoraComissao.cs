using Desafio1.Comissoes.Models;

namespace Desafio1.Comissoes.Services;

public static class CalculadoraComissao
{
    private const decimal LimiteSemComissao = 100m;
    private const decimal LimiteComissaoMaior = 500m;
    private const decimal PercentualMenor = 0.01m;
    private const decimal PercentualMaior = 0.05m;

    // Aplica a regra de comissão a uma única venda
    public static decimal CalcularComissao(decimal valorVenda)
    {
        if (valorVenda < LimiteSemComissao)
            return 0m;

        if (valorVenda < LimiteComissaoMaior)
            return valorVenda * PercentualMenor;

        return valorVenda * PercentualMaior;
    }

    // Agrupa as vendas por vendedor e soma o total vendido e a comissão de cada um
    public static List<ResumoVendedor> CalcularPorVendedor(IEnumerable<Venda> vendas)
    {
        return vendas
            .GroupBy(v => v.Vendedor)
            .Select(grupo => new ResumoVendedor(
                grupo.Key,
                grupo.Count(),
                grupo.Sum(v => v.Valor),
                Math.Round(grupo.Sum(v => CalcularComissao(v.Valor)), 2, MidpointRounding.AwayFromZero)))
            .OrderByDescending(r => r.TotalComissao)
            .ToList();
    }
}
