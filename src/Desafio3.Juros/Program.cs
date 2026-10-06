using System.Globalization;
using System.Text;
using Desafio3.Juros.Services;

Console.OutputEncoding = Encoding.UTF8;
var culturaBr = new CultureInfo("pt-BR");

Console.WriteLine("=== CÁLCULO DE JUROS POR ATRASO (2,5% ao dia) ===");
Console.WriteLine();

var valor = LerValor("Valor (ex.: 1500,50): ");
var vencimento = LerData("Data de vencimento (dd/mm/aaaa): ");
var hoje = DateOnly.FromDateTime(DateTime.Today);

var resultado = CalculadoraJuros.Calcular(valor, vencimento, hoje);

Console.WriteLine();
Console.WriteLine($"Data de hoje:     {hoje:dd/MM/yyyy}");
Console.WriteLine($"Valor original:   {valor.ToString("C", culturaBr)}");
Console.WriteLine($"Dias em atraso:   {resultado.DiasAtraso}");
Console.WriteLine($"Juros:            {resultado.Juros.ToString("C", culturaBr)}");
Console.WriteLine($"Valor atualizado: {resultado.ValorAtualizado.ToString("C", culturaBr)}");

// Lê um valor monetário no formato brasileiro, repetindo até ser válido
decimal LerValor(string mensagem)
{
    while (true)
    {
        Console.Write(mensagem);
        var entrada = Console.ReadLine() ?? throw new InvalidOperationException("Entrada encerrada.");

        if (decimal.TryParse(entrada.Replace("R$", "").Trim(), NumberStyles.Number, culturaBr, out var v) && v > 0)
            return v;

        Console.WriteLine("Valor inválido. Use números maiores que zero, ex.: 1500,50");
    }
}

// Lê uma data no formato dd/MM/yyyy, repetindo até ser válida
DateOnly LerData(string mensagem)
{
    while (true)
    {
        Console.Write(mensagem);
        var entrada = Console.ReadLine() ?? throw new InvalidOperationException("Entrada encerrada.");

        if (DateOnly.TryParseExact(entrada.Trim(), "dd/MM/yyyy", culturaBr, DateTimeStyles.None, out var data))
            return data;

        Console.WriteLine("Data inválida. Use o formato dd/mm/aaaa.");
    }
}
