using System.Text.Json;
using TargetSistemas.Desafio.Modelos;

namespace TargetSistemas.Desafio.Desafios;

public static class Desafio2Estoque
{
    private static List<Produto> produtos = new List<Produto>();
    private static List<Movimentacao> movimentacoes = new List<Movimentacao>();
    private static int proximoId = 1;

    public static void Executar()
    {
        GarantirProdutosCarregados();

        while (true)
        {
            Console.WriteLine("\n=======================================================");
            Console.WriteLine("        DESAFIO 2: CONTROLE E MOVIMENTAÇÃO DE ESTOQUE");
            Console.WriteLine("=======================================================");
            Console.WriteLine(" [1] Ver estoque atual dos produtos");
            Console.WriteLine(" [2] Lançar nova movimentação (Entrada/Saída)");
            Console.WriteLine(" [3] Ver histórico de movimentações");
            Console.WriteLine(" [0] Voltar ao menu principal");
            Console.WriteLine("=======================================================");
            Console.Write(" Escolha uma opção: ");

            string? opcao = Console.ReadLine()?.Trim();

            if (opcao == "0" || opcao == null)
            {
                break;
            }

            switch (opcao)
            {
                case "1":
                    ExibirEstoqueAtual();
                    break;

                case "2":
                    RealizarMovimentacao();
                    break;

                case "3":
                    ExibirHistorico();
                    break;

                default:
                    Console.WriteLine("\nOpção inválida!");
                    break;
            }
        }
    }

    private static void GarantirProdutosCarregados()
    {
        if (produtos.Count > 0)
        {
            return;
        }

        string caminhoArquivo = Path.Combine(AppContext.BaseDirectory, "Dados", "estoque.json");

        if (!File.Exists(caminhoArquivo))
        {
            Console.WriteLine("Arquivo estoque.json não encontrado!");
            return;
        }

        string json = File.ReadAllText(caminhoArquivo);
        var opcoes = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        DadosEstoque? dados = JsonSerializer.Deserialize<DadosEstoque>(json, opcoes);

        if (dados != null && dados.Estoque.Count > 0)
        {
            produtos = dados.Estoque;
        }
    }

    private static void ExibirEstoqueAtual()
    {
        Console.WriteLine("\n------------------- ESTOQUE ATUAL -------------------");
        Console.WriteLine("{0,-8} | {1,-30} | {2,10}", "Código", "Descrição", "Quantidade");
        Console.WriteLine("------------------------------------------------------");

        foreach (var produto in produtos)
        {
            Console.WriteLine("{0,-8} | {1,-30} | {2,10}",
                produto.CodigoProduto,
                produto.DescricaoProduto,
                produto.Estoque);
        }

        Console.WriteLine("------------------------------------------------------");
    }

    private static void RealizarMovimentacao()
    {
        ExibirEstoqueAtual();

        Console.Write("\nDigite o código do produto que deseja movimentar: ");
        if (!int.TryParse(Console.ReadLine(), out int codigo))
        {
            Console.WriteLine("Código inválido! Digite apenas números.");
            return;
        }

        Produto? produtoSelecionado = null;

        foreach (var p in produtos)
        {
            if (p.CodigoProduto == codigo)
            {
                produtoSelecionado = p;
                break;
            }
        }

        if (produtoSelecionado == null)
        {
            Console.WriteLine($"Produto com código {codigo} não foi encontrado.");
            return;
        }

        Console.WriteLine($"\nProduto selecionado: {produtoSelecionado.DescricaoProduto} (Estoque atual: {produtoSelecionado.Estoque})");

        Console.WriteLine("Tipo de movimentação:");
        Console.WriteLine(" [1] Entrada (+)");
        Console.WriteLine(" [2] Saída (-)");
        Console.Write("Escolha o tipo: ");
        string? tipoOpcao = Console.ReadLine()?.Trim();

        string tipo;
        if (tipoOpcao == "1")
        {
            tipo = "Entrada";
        }
        else if (tipoOpcao == "2")
        {
            tipo = "Saída";
        }
        else
        {
            Console.WriteLine("Tipo inválido. Operação cancelada.");
            return;
        }

        Console.Write("Quantidade a movimentar: ");
        if (!int.TryParse(Console.ReadLine(), out int quantidade) || quantidade <= 0)
        {
            Console.WriteLine("A quantidade deve ser um número inteiro maior que zero!");
            return;
        }

        Console.Write("Descrição/Motivo da movimentação: ");
        string? descricao = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(descricao))
        {
            descricao = tipo == "Entrada" ? "Entrada no estoque" : "Saída do estoque";
        }

        if (tipo == "Saída" && quantidade > produtoSelecionado.Estoque)
        {
            Console.WriteLine($"\n[ERRO] Estoque insuficiente! Você tentou retirar {quantidade} unidade(s), mas o saldo atual é {produtoSelecionado.Estoque}.");
            return;
        }

        int estoqueAnterior = produtoSelecionado.Estoque;

        if (tipo == "Entrada")
        {
            produtoSelecionado.Estoque += quantidade;
        }
        else
        {
            produtoSelecionado.Estoque -= quantidade;
        }

        Movimentacao registro = new Movimentacao
        {
            Id = proximoId++,
            CodigoProduto = produtoSelecionado.CodigoProduto,
            DescricaoProduto = produtoSelecionado.DescricaoProduto,
            Tipo = tipo,
            Descricao = descricao,
            Quantidade = quantidade,
            EstoqueAnterior = estoqueAnterior,
            EstoqueFinal = produtoSelecionado.Estoque
        };

        movimentacoes.Add(registro);

        Console.WriteLine("\n=======================================================");
        Console.WriteLine($" MOVIMENTAÇÃO #{registro.Id} REGISTRADA COM SUCESSO!");
        Console.WriteLine("=======================================================");
        Console.WriteLine($" Produto:           {registro.DescricaoProduto} (Cód: {registro.CodigoProduto})");
        Console.WriteLine($" Tipo:              {registro.Tipo}");
        Console.WriteLine($" Descrição/Motivo:  {registro.Descricao}");
        Console.WriteLine($" Quantidade:        {registro.Quantidade}");
        Console.WriteLine($" Estoque Anterior:  {registro.EstoqueAnterior}");
        Console.WriteLine($" Estoque Final:     {registro.EstoqueFinal}");
        Console.WriteLine("=======================================================");
    }

    private static void ExibirHistorico()
    {
        if (movimentacoes.Count == 0)
        {
            Console.WriteLine("\nNenhuma movimentação foi realizada até o momento.");
            return;
        }

        Console.WriteLine("\n---------------- HISTÓRICO DE MOVIMENTAÇÕES ----------------");
        Console.WriteLine("{0,-4} | {1,-20} | {2,-8} | {3,4} | {4,8} -> {5,-8} | {6}",
            "ID", "Produto", "Tipo", "Qtd", "Ant.", "Final", "Descrição");
        Console.WriteLine("-------------------------------------------------------------------------------------");

        foreach (var mov in movimentacoes)
        {
            Console.WriteLine("{0,-4} | {1,-20} | {2,-8} | {3,4} | {4,8} -> {5,-8} | {6}",
                mov.Id,
                mov.DescricaoProduto,
                mov.Tipo,
                mov.Quantidade,
                mov.EstoqueAnterior,
                mov.EstoqueFinal,
                mov.Descricao);
        }

        Console.WriteLine("-------------------------------------------------------------------------------------");
    }
}
