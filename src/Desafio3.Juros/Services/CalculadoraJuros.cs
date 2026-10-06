namespace Desafio3.Juros.Services;

public record ResultadoJuros(int DiasAtraso, decimal Juros, decimal ValorAtualizado);

public static class CalculadoraJuros
{
    private const decimal TaxaDiaria = 0.025m; // 2,5% ao dia

    // Calcula juros simples sobre os dias em atraso entre o vencimento e a data de referência
    public static ResultadoJuros Calcular(decimal valor, DateOnly vencimento, DateOnly dataReferencia)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor deve ser maior que zero.");

        // Pago em dia ou antecipado não gera juros
        var diasAtraso = Math.Max(0, dataReferencia.DayNumber - vencimento.DayNumber);

        var juros = Math.Round(valor * TaxaDiaria * diasAtraso, 2, MidpointRounding.AwayFromZero);

        return new ResultadoJuros(diasAtraso, juros, valor + juros);
    }
}
