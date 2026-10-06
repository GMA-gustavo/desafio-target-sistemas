using System.Globalization;
using System.Text;
using System.Text.Json;
using Desafio1.Comissoes.Models;
using Desafio1.Comissoes.Services;

Console.OutputEncoding = Encoding.UTF8;
var culturaBr = new CultureInfo("pt-BR");

var caminhoArquivo = Path.Combine(AppContext.BaseDirectory, "Data", "vendas.json");
var opcoesJson = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

var arquivo = JsonSerializer.Deserialize<ArquivoVendas>(File.ReadAllText(caminhoArquivo), opcoesJson);
if (arquivo is null || arquivo.Vendas.Count == 0)
{
    Console.WriteLine("Nenhuma venda encontrada no arquivo.");
    return;
}

var resumos = CalculadoraComissao.CalcularPorVendedor(arquivo.Vendas);

Console.WriteLine("=== COMISSÃO POR VENDEDOR ===");
Console.WriteLine();
Console.WriteLine($"{"Vendedor",-18}{"Vendas",8}{"Total vendido",18}{"Comissão",14}");
Console.WriteLine(new string('-', 58));

foreach (var r in resumos)
{
    Console.WriteLine(
        $"{r.Vendedor,-18}{r.QuantidadeVendas,8}{r.TotalVendido.ToString("C", culturaBr),18}{r.TotalComissao.ToString("C", culturaBr),14}");
}

Console.WriteLine(new string('-', 58));
Console.WriteLine($"{"TOTAL",-18}{resumos.Sum(r => r.QuantidadeVendas),8}" +
                  $"{resumos.Sum(r => r.TotalVendido).ToString("C", culturaBr),18}" +
                  $"{resumos.Sum(r => r.TotalComissao).ToString("C", culturaBr),14}");
