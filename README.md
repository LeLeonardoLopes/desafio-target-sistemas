# Desafio Técnico - Target Sistemas

Este repositório contém a resolução dos três exercícios propostos no teste técnico de desenvolvimento. O projeto foi desenvolvido em **C# (.NET 8)** no formato de aplicação de console interativa.

---

## Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) instalado.

---

## Como Executar

1. Clone o repositório em sua máquina:
   ```bash
   git clone https://github.com/LeLeonardoLopes/desafio-target-sistemas.git
   cd desafio-target-sistemas
   ```

2. Execute o projeto utilizando o comando da CLI do .NET:
   ```bash
   dotnet run --project src/TargetSistemas.Desafio
   ```
   *(Ou abra a solução `TargetSistemas.sln` no Visual Studio / VS Code e execute o projeto `TargetSistemas.Desafio`)*.

---

## Funcionalidades e Desafios

Ao executar o programa, um menu interativo será exibido no console para navegar entre os três desafios:

### 1. Cálculo de Comissão de Vendedores
- **Entrada:** Lê os registros de vendas a partir do arquivo `Dados/vendas.json`.
- **Regra de Negócio:** Aplica faixas de comissão para cada venda individual:
  - Abaixo de R$ 100,00: sem comissão.
  - De R$ 100,00 a R$ 499,99: 1% de comissão.
  - A partir de R$ 500,00: 5% de comissão.
- **Saída:** Exibe uma tabela consolidada com a quantidade de vendas, o total vendido e o total de comissão por vendedor, além do somatório geral da equipe.

### 2. Controle e Movimentação de Estoque
- **Entrada:** Inicializa os dados a partir de `Dados/estoque.json` com os produtos e saldos do depósito.
- **Operações:**
  - Consulta do saldo atual dos produtos.
  - Lançamento de movimentações de **Entrada** (adição ao estoque) ou **Saída** (baixa no estoque).
  - Consulta do histórico de todas as movimentações realizadas durante a execução.
- **Regras:**
  - Cada movimentação recebe um identificador numérico único sequencial.
  - Exige uma descrição/motivo para a operação.
  - Validações: impede valores negativos/zerados e bloqueia saídas caso a quantidade solicitada seja superior ao saldo disponível em estoque.
  - Retorna o saldo anterior e a quantidade final atualizada após cada lançamento.

### 3. Cálculo de Juros por Atraso
- **Entrada:** Solicita ao usuário o valor original do boleto/título e a respectiva data de vencimento.
- **Regra de Negócio:**
  - Compara a data de vencimento informada com a data atual (`DateTime.Today`).
  - Caso o título já esteja vencido, calcula a quantidade exata de dias de atraso e aplica a taxa de **2,5% ao dia**.
  - Caso a data de vencimento seja hoje ou futura, informa que o título está em dia e não aplica juros.
- **Saída:** Apresenta o detalhamento com valor original, dias de atraso, valor dos juros calculados e valor total a ser pago.

---

## Estrutura do Projeto

```text
├── TargetSistemas.sln
├── README.md
├── .gitignore
└── src
    └── TargetSistemas.Desafio
        ├── Program.cs             # Ponto de entrada e menu interativo
        ├── TargetSistemas.Desafio.csproj
        ├── Dados                  # Arquivos JSON fornecidos no teste
        │   ├── estoque.json
        │   └── vendas.json
        ├── Desafios               # Lógica de cada um dos exercícios
        │   ├── Desafio1Comissao.cs
        │   ├── Desafio2Estoque.cs
        │   └── Desafio3Juros.cs
        └── Modelos                # Classes e estruturas de dados
            ├── Movimentacao.cs
            ├── Produto.cs
            ├── TotalVendedor.cs
            └── Venda.cs
```
