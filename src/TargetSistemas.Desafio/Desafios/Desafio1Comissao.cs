using System.Text.Json;
using TargetSistemas.Desafio.Modelos;

namespace TargetSistemas.Desafio.Desafios;

public static class Desafio1Comissao
{
    public static void Executar()
    {
        Console.WriteLine("\n=======================================================");
        Console.WriteLine("    DESAFIO 1: CÁLCULO DE COMISSÃO DOS VENDEDORES");
        Console.WriteLine("=======================================================\n");

        string caminhoArquivo = Path.Combine(AppContext.BaseDirectory, "Dados", "vendas.json");

        if (!File.Exists(caminhoArquivo))
        {
            Console.WriteLine("Arquivo vendas.json não foi encontrado!");
            return;
        }

        string json = File.ReadAllText(caminhoArquivo);
        var opcoes = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        DadosVendas? dados = JsonSerializer.Deserialize<DadosVendas>(json, opcoes);

        if (dados == null || dados.Vendas.Count == 0)
        {
            Console.WriteLine("Nenhuma venda encontrada.");
            return;
        }

        List<TotalVendedor> listaTotais = new List<TotalVendedor>();

        foreach (var venda in dados.Vendas)
        {
            decimal comissaoDaVenda = CalcularComissao(venda.Valor);

            TotalVendedor? vendedorExistente = null;

            foreach (var total in listaTotais)
            {
                if (total.Nome == venda.Vendedor)
                {
                    vendedorExistente = total;
                    break;
                }
            }

            if (vendedorExistente != null)
            {
                vendedorExistente.QuantidadeVendas++;
                vendedorExistente.TotalVendido += venda.Valor;
                vendedorExistente.TotalComissao += comissaoDaVenda;
            }
            else
            {
                TotalVendedor novoVendedor = new TotalVendedor
                {
                    Nome = venda.Vendedor,
                    QuantidadeVendas = 1,
                    TotalVendido = venda.Valor,
                    TotalComissao = comissaoDaVenda
                };

                listaTotais.Add(novoVendedor);
            }
        }

        Console.WriteLine("{0,-20} | {1,8} | {2,16} | {3,16}", "Vendedor", "Vendas", "Total Vendido", "Comissão Total");
        Console.WriteLine("--------------------------------------------------------------------");

        decimal somaTotalVendas = 0;
        decimal somaTotalComissao = 0;
        int somaQuantidadeVendas = 0;

        foreach (var vendedor in listaTotais)
        {
            Console.WriteLine("{0,-20} | {1,8} | {2,16:C2} | {3,16:C2}",
                vendedor.Nome,
                vendedor.QuantidadeVendas,
                vendedor.TotalVendido,
                vendedor.TotalComissao);

            somaQuantidadeVendas += vendedor.QuantidadeVendas;
            somaTotalVendas += vendedor.TotalVendido;
            somaTotalComissao += vendedor.TotalComissao;
        }

        Console.WriteLine("--------------------------------------------------------------------");
        Console.WriteLine("{0,-20} | {1,8} | {2,16:C2} | {3,16:C2}",
            "TOTAL GERAL",
            somaQuantidadeVendas,
            somaTotalVendas,
            somaTotalComissao);

        Console.WriteLine("\nRegras de comissão aplicadas por venda:");
        Console.WriteLine(" - Menor que R$ 100,00: sem comissão");
        Console.WriteLine(" - Entre R$ 100,00 e R$ 499,99: 1% de comissão");
        Console.WriteLine(" - A partir de R$ 500,00: 5% de comissão");
    }

    public static decimal CalcularComissao(decimal valor)
    {
        if (valor < 100)
        {
            return 0;
        }
        else if (valor < 500)
        {
            return valor * 0.01m;
        }
        else
        {
            return valor * 0.05m;
        }
    }
}
