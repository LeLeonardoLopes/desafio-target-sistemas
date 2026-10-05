namespace TargetSistemas.Desafio.Desafios;

public static class Desafio3Juros
{
    private const decimal TaxaJurosDiaria = 0.025m;

    public static void Executar()
    {
        Console.WriteLine("\n=======================================================");
        Console.WriteLine("        DESAFIO 3: CÁLCULO DE JUROS POR ATRASO");
        Console.WriteLine("=======================================================\n");

        Console.Write("Digite o valor original do boleto/título (Ex: 1000,00): ");
        string? entradaValor = Console.ReadLine()?.Trim();

        if (!decimal.TryParse(entradaValor?.Replace(".", ","), out decimal valorOriginal) || valorOriginal <= 0)
        {
            Console.WriteLine("Valor inválido! Digite um número positivo maior que zero.");
            return;
        }

        Console.Write("Digite a data de vencimento (dd/MM/aaaa): ");
        string? entradaData = Console.ReadLine()?.Trim();

        if (!DateTime.TryParse(entradaData, out DateTime dataVencimento))
        {
            Console.WriteLine("Data inválida! Digite uma data no formato dia/mês/ano.");
            return;
        }

        DateTime dataHoje = DateTime.Today;

        Console.WriteLine("\n---------------- RESULTADO DO CÁLCULO ----------------");
        Console.WriteLine($"Valor Original:       {valorOriginal:C2}");
        Console.WriteLine($"Data de Vencimento:   {dataVencimento:dd/MM/yyyy}");
        Console.WriteLine($"Data Atual (Hoje):    {dataHoje:dd/MM/yyyy}");

        if (dataVencimento >= dataHoje)
        {
            Console.WriteLine("\nStatus: Em dia (dentro do prazo de vencimento)");
            Console.WriteLine("Dias de atraso:       0");
            Console.WriteLine($"Valor dos Juros:      {0m:C2}");
            Console.WriteLine($"Total a Pagar:        {valorOriginal:C2}");
        }
        else
        {
            int diasAtraso = (dataHoje - dataVencimento.Date).Days;
            decimal valorJuros = valorOriginal * TaxaJurosDiaria * diasAtraso;
            decimal valorTotal = valorOriginal + valorJuros;

            Console.WriteLine("\nStatus: TÍTULO VENCIDO");
            Console.WriteLine($"Dias de atraso:       {diasAtraso} dia(s)");
            Console.WriteLine($"Taxa diária:          2,5% ao dia");
            Console.WriteLine($"Valor dos Juros:      {valorJuros:C2}");
            Console.WriteLine($"Valor Total a Pagar:  {valorTotal:C2}");
        }

        Console.WriteLine("------------------------------------------------------");
    }
}
