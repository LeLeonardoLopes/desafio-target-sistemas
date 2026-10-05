using TargetSistemas.Desafio.Desafios;

while (true)
{
    Console.WriteLine("=======================================================");
    Console.WriteLine("         TESTE TÉCNICO - TARGET SISTEMAS");
    Console.WriteLine("=======================================================");
    Console.WriteLine(" [1] Desafio 1: Cálculo de Comissão de Vendedores");
    Console.WriteLine(" [2] Desafio 2: Movimentação de Estoque");
    Console.WriteLine(" [3] Desafio 3: Cálculo de Juros por Atraso");
    Console.WriteLine(" [0] Sair");
    Console.WriteLine("=======================================================");
    Console.Write(" Escolha uma opção: ");

    string? opcao = Console.ReadLine()?.Trim();

    if (opcao == null)
    {
        break;
    }

    switch (opcao)
    {
        case "1":
            Desafio1Comissao.Executar();
            AguardarEnter();
            break;

        case "2":
            Desafio2Estoque.Executar();
            AguardarEnter();
            break;

        case "3":
            Desafio3Juros.Executar();
            AguardarEnter();
            break;

        case "0":
            Console.WriteLine("\nEncerrando o programa. Até logo!");
            return;

        default:
            Console.WriteLine("\nOpção inválida!");
            AguardarEnter();
            break;
    }
}

static void AguardarEnter()
{
    Console.WriteLine("\nPressione [ENTER] para continuar...");
    Console.ReadLine();
}
