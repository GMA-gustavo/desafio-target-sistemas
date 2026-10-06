using System.Text;
using System.Text.Json;
using Desafio2.Estoque.Models;
using Desafio2.Estoque.Services;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

var caminhoArquivo = Path.Combine(AppContext.BaseDirectory, "Data", "estoque.json");
var opcoesJson = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
var arquivo = JsonSerializer.Deserialize<ArquivoEstoque>(File.ReadAllText(caminhoArquivo), opcoesJson);

var controle = new ControleEstoque(arquivo?.Estoque ?? new List<Produto>());

while (true)
{
    Console.WriteLine();
    Console.WriteLine("=== CONTROLE DE ESTOQUE ===");
    Console.WriteLine("1 - Listar produtos");
    Console.WriteLine("2 - Lançar entrada");
    Console.WriteLine("3 - Lançar saída");
    Console.WriteLine("4 - Histórico de movimentações");
    Console.WriteLine("0 - Sair");

    var opcao = LerTexto("Opção: ");
    switch (opcao)
    {
        case "1": ListarProdutos(); break;
        case "2": LancarMovimentacao(TipoMovimentacao.Entrada); break;
        case "3": LancarMovimentacao(TipoMovimentacao.Saida); break;
        case "4": ListarMovimentacoes(); break;
        case "0": return;
        default: Console.WriteLine("Opção inválida."); break;
    }
}

void ListarProdutos()
{
    Console.WriteLine();
    Console.WriteLine($"{"Código",-8}{"Produto",-30}{"Estoque",8}");
    foreach (var p in controle.Produtos)
        Console.WriteLine($"{p.CodigoProduto,-8}{p.DescricaoProduto,-30}{p.Estoque,8}");
}

// Coleta os dados da movimentação, registra e mostra o estoque final do produto
void LancarMovimentacao(TipoMovimentacao tipo)
{
    var codigo = LerInteiro("Código do produto: ");
    var quantidade = LerInteiro("Quantidade: ");
    var descricao = LerTexto("Descrição (ex.: compra de fornecedor, venda, devolução): ");

    try
    {
        var mov = controle.Movimentar(codigo, tipo, quantidade, descricao);
        var produto = controle.Produtos.First(p => p.CodigoProduto == mov.CodigoProduto);

        Console.WriteLine();
        Console.WriteLine($"Movimentação #{mov.Id} registrada ({mov.Tipo.ParaTexto()}: {mov.Quantidade} un. - {mov.Descricao}).");
        Console.WriteLine($"Estoque final de {produto.DescricaoProduto}: {mov.EstoqueFinal}");
    }
    catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }
}

void ListarMovimentacoes()
{
    if (controle.Movimentacoes.Count == 0)
    {
        Console.WriteLine("Nenhuma movimentação lançada.");
        return;
    }

    Console.WriteLine();
    Console.WriteLine($"{"ID",-5}{"Data",-18}{"Produto",-9}{"Tipo",-9}{"Qtde",6}{"Saldo",7}  Descrição");
    foreach (var m in controle.Movimentacoes)
        Console.WriteLine($"{m.Id,-5}{m.Data:dd/MM/yyyy HH:mm}  {m.CodigoProduto,-9}{m.Tipo.ParaTexto(),-9}{m.Quantidade,6}{m.EstoqueFinal,7}  {m.Descricao}");
}

// Lê uma linha do console; encerra o programa se a entrada terminar (Ctrl+Z / fim de arquivo)
static string LerTexto(string mensagem)
{
    Console.Write(mensagem);
    var entrada = Console.ReadLine();
    if (entrada is null)
        Environment.Exit(0);
    return entrada.Trim();
}

// Repete a pergunta até o usuário digitar um número inteiro válido
static int LerInteiro(string mensagem)
{
    while (true)
    {
        if (int.TryParse(LerTexto(mensagem), out var valor))
            return valor;
        Console.WriteLine("Digite um número inteiro válido.");
    }
}
